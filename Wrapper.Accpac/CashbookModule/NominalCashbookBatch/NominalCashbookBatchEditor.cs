using AccpacCOMAPI;
using Microsoft.Extensions.Logging;
using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels;
using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels.NominalCashbookBatchModels;
using Wrapper.Models.Common;
using Wrapper.Models.Common.Exceptions;
using Wrapper.Services;
using Wrapper.Services.Accpac.CashbookModule;
using Wrapper.Services.Accpac.CashbookModule.NominalCashbookBatch;
using Wrapper.Services.Common;
using Wrapper.Services.Utils;

namespace Wrapper.Accpac.CashbookModule.NominalCashbookBatch
{
    public class NominalCashbookBatchEditor : INominalCashbookBatchEditor
    {
        private readonly ICashbookBatchViewComposer viewComposer;
        private readonly ILogger<NominalCashbookBatchEditor> logger;
        private readonly INominalCashbookBatchValidator validator;
        private readonly INotificationMessenger messenger;

        public NominalCashbookBatchEditor(
                ICashbookBatchViewComposer viewComposer,
                ILogger<NominalCashbookBatchEditor> logger,
                INominalCashbookBatchValidator validator,
                INotificationMessenger messenger
            )
        {
            this.viewComposer = viewComposer;
            this.logger = logger;
            this.validator = validator;
            this.messenger = messenger;
        }

        public async Task CreateNominalCashbookBatchAsync(IOperationContext context, NominalCashbookBatchEntryModel model)
        {
            await validator.ValidateBatchAsync(context, model);

            int currentEntry = 0;
            int totalEntries = model.Headers.Count;

            context.AccpacDBLink.TransactionBegin(out int p);

            try
            {
                CashbookBatchView view = viewComposer.BuildBatchView(context);

                view.BatchView.RecordCreate(tagViewRecordCreateEnum.VIEW_RECORD_CREATE_INSERT);
                view.BatchView.Fields.FieldByName["BANKCODE"].set_Value(model.BankCode);
                view.BatchView.Fields.FieldByName["TEXTDESC"].PutWithoutVerification(model.BatchName);
                view.BatchView.Update();

                foreach (CashbookBatchHeaderEntryModel header in model.Headers)
                {
                    // notify progress
                    currentEntry++;
                    await messenger.NotifyProgressAsync(context, new NotificationModel
                    (
                        currentEntry, totalEntries, ProgressType.Import, LongRunningProcessType.NominalcashbookAccpacPosting, context.UserId
                    ));

                    view.HeaderView.RecordCreate(tagViewRecordCreateEnum.VIEW_RECORD_CREATE_NOINSERT);
                    view.HeaderView.Fields.FieldByName["DATE"].set_Value(header.EntryDate);

                    view.HeaderView.Fields.FieldByName["BT2GLCURSR"].set_Value(header.Currency);
                    view.HeaderView.Fields.FieldByName["BT2GLRATE"].set_Value(header.ExchangeRate);
                    view.HeaderView.Fields.FieldByName["BK2GLRATE"].set_Value(header.ExchangeRate);

                    view.HeaderView.Fields.FieldByName["REFERENCE"].PutWithoutVerification(header.ReferenceNo);
                    view.HeaderView.Fields.FieldByName["TEXTDESC"].PutWithoutVerification(header.Description);
                    view.HeaderView.Process();

                    foreach (CashbookBatchEntryDetailModel detail in header.Details)
                    {
                        view.DetailView.RecordCreate(tagViewRecordCreateEnum.VIEW_RECORD_CREATE_NOINSERT);
                        view.DetailView.Fields.FieldByName["SRCECODE"].set_Value(detail.SourceCode);
                        view.DetailView.Fields.FieldByName["TEXTDESC"].PutWithoutVerification(detail.Description);
                        view.DetailView.Fields.FieldByName["ACCTID"].set_Value(detail.AccountCode);

                        if (detail.DebitAmount.HasValue)
                        {
                            view.DetailView.Fields.FieldByName["DEBITAMT"].set_Value(detail.DebitAmount.Value);
                        }

                        if (detail.CreditAmount.HasValue)
                        {
                            view.DetailView.Fields.FieldByName["CREDITAMT"].set_Value(detail.CreditAmount.Value);
                        }

                        view.DetailView.Insert();

                        view.HeaderView.Insert();
                        view.BatchView.Update();

                        view.DetailView.RecordClear();
                    }

                    view.HeaderView.RecordClear();
                }

                context.AccpacDBLink.TransactionCommit(out p);
            }
            catch (Exception ex)
            {
                context.AccpacDBLink.TransactionRollback(out p);

                ErrorUtils.LogAndThrowAccpacException(context, logger, "Failed to create cashbook nominal batch",
                    unhandledExceptionAction: () => throw new CreateEntityException(ex.Message, ex.InnerException));
            }
            finally
            {
                // make sure the progress is 100% even if an error occurs
                await messenger.NotifyProgressAsync(context, new NotificationModel
                (
                    totalEntries, totalEntries, ProgressType.Import, LongRunningProcessType.NominalcashbookAccpacPosting, context.UserId
                ));
            }
        }
    }
}
