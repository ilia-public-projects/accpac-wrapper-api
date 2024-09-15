using AccpacCOMAPI;
using Microsoft.Extensions.Logging;
using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels;
using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels.ApCashbookBatchModels;
using Wrapper.Models.Common;
using Wrapper.Models.Common.Exceptions;
using Wrapper.Services;
using Wrapper.Services.Accpac.CashbookModule;
using Wrapper.Services.Accpac.CashbookModule.ApCashbookBatchServices;
using Wrapper.Services.Common;
using Wrapper.Services.Utils;

namespace Wrapper.Accpac.CashbookModule.ApCashbookBatchServices
{
    public class ApCashbookBatchEditor : IApCashbookBatchEditor
    {
        private readonly ICashbookBatchViewComposer viewComposer;
        private readonly ILogger<ApCashbookBatchEditor> logger;
        private readonly IApCashbookBatchValidator validator;
        private readonly INotificationMessenger messenger;

        public ApCashbookBatchEditor(
                ICashbookBatchViewComposer viewComposer,
                ILogger<ApCashbookBatchEditor> logger,
                IApCashbookBatchValidator validator,
                INotificationMessenger messenger
            )
        {
            this.viewComposer = viewComposer;
            this.logger = logger;
            this.validator = validator;
            this.messenger = messenger;
        }

        public async Task CreateBatchAsync(IOperationContext context, ApCashbookBatchEntryModel model)
        {
            await validator.ValidateBatchAsync(context, model);

            int currentEntry = 0;
            int totalEntries = model.Headers.Count;

            context.AccpacDBLink.TransactionBegin(out int p);
            try
            {

                CashbookBatchView view = viewComposer.BuildBatchView(context);

                view.BatchView.RecordCreate(tagViewRecordCreateEnum.VIEW_RECORD_CREATE_INSERT);
                view.BatchView.Fields.FieldByName["BANKCODE"].PutWithoutVerification(model.BankCode);
                view.BatchView.Fields.FieldByName["TEXTDESC"].PutWithoutVerification(model.BatchName);
                view.BatchView.Update();

                foreach (ApCashbookBatchHeaderEntryModel header in model.Headers)
                {
                    // notify progress
                    currentEntry++;
                    await messenger.NotifyProgressAsync(context, new NotificationModel
                    (
                        currentEntry, totalEntries, ProgressType.Import, LongRunningProcessType.APcashbookAccpacPosting, context.UserId
                    ));

                    // create header
                    view.HeaderView.RecordCreate(tagViewRecordCreateEnum.VIEW_RECORD_CREATE_INSERT);
                    view.HeaderView.Fields.FieldByName["DATE"].PutWithoutVerification(header.EntryDate);
                    view.HeaderView.Fields.FieldByName["ENTRYTYPE"].PutWithoutVerification("1"); // AP
                    view.HeaderView.Fields.FieldByName["MISCCODE"].PutWithoutVerification(header.MiscCode);

                    view.HeaderView.Fields.FieldByName["BT2GLCURSR"].PutWithoutVerification(header.Currency);
                    view.HeaderView.Fields.FieldByName["BT2GLRATE"].PutWithoutVerification(header.ExchangeRate);
                    view.HeaderView.Fields.FieldByName["BK2GLRATE"].PutWithoutVerification(header.ExchangeRate);

                    view.HeaderView.Fields.FieldByName["REFERENCE"].PutWithoutVerification(header.ReferenceNo);
                    view.HeaderView.Fields.FieldByName["TEXTDESC"].PutWithoutVerification(header.Description);

                    // header optional fields

                    view.HeaderOptFieldsView.RecordCreate(tagViewRecordCreateEnum.VIEW_RECORD_CREATE_INSERT); 
                    view.HeaderOptFieldsView.Fields.FieldByName["OPTFIELD"].PutWithoutVerification("CREDITCODE");
                    view.HeaderOptFieldsView.Fields.FieldByName["SWSET"].PutWithoutVerification("1");    
                    view.HeaderOptFieldsView.Fields.FieldByName["VALIFTEXT"].PutWithoutVerification(header.CreditCode);
                    view.HeaderOptFieldsView.Insert();

                    foreach (CashbookBatchEntryDetailModel detail in header.Details)
                    {
                        view.DetailView.RecordCreate(tagViewRecordCreateEnum.VIEW_RECORD_CREATE_NOINSERT);
                        view.DetailView.Fields.FieldByName["SRCECODE"].PutWithoutVerification(detail.SourceCode);
                        view.DetailView.Fields.FieldByName["TEXTDESC"].PutWithoutVerification(detail.Description);
                        view.DetailView.Fields.FieldByName["ALLOCMODE"].PutWithoutVerification("1"); //Allocation mode = prepayment

                        decimal amount = 0;
                        if (detail.DebitAmount.HasValue)
                        {
                            amount = detail.DebitAmount.Value;
                            view.DetailView.Fields.FieldByName["DEBITAMT"].set_Value(detail.DebitAmount.Value);
                        }

                        if (detail.CreditAmount.HasValue)
                        {
                            amount = detail.CreditAmount.Value;
                            view.DetailView.Fields.FieldByName["CREDITAMT"].set_Value(detail.CreditAmount.Value);
                        }

                        view.DetailView.Insert();

                        view.SubDetailView.RecordCreate(tagViewRecordCreateEnum.VIEW_RECORD_CREATE_INSERT);
                        view.SubDetailView.Fields.FieldByName["DOCNUMBER"].PutWithoutVerification("0");
                        view.SubDetailView.Fields.FieldByName["PAYNUMBER"].PutWithoutVerification(1);
                        view.SubDetailView.Fields.FieldByName["DOCTYPE"].PutWithoutVerification(0);
                        view.SubDetailView.Fields.FieldByName["APPLAMOUNT"].set_Value(amount);
                        view.SubDetailView.Fields.FieldByName["IDCUST"].PutWithoutVerification(header.MiscCode);
                        view.SubDetailView.Fields.FieldByName["ENTRYTYPE"].PutWithoutVerification("1");

                        view.SubDetailView.Insert();

                        view.DetailView.Update(); 

                        view.DetailView.RecordClear();
                        view.SubDetailView.RecordClear();
                    }

                    view.HeaderView.Insert();
                    view.HeaderOptFieldsView.RecordClear(); 
                    view.HeaderView.RecordClear(); 
                }

                context.AccpacDBLink.TransactionCommit(out p);
            }
            catch (Exception ex)
            {
                context.AccpacDBLink.TransactionRollback(out p);

                ErrorUtils.LogAndThrowAccpacException(context, logger, "Failed to create cashbook AP batch",
                    unhandledExceptionAction: () => throw new CreateEntityException(ex.Message, ex.InnerException));
            }
            finally 
            {
                // make sure the progress is 100% even if an error occurs
                await messenger.NotifyProgressAsync(context, new NotificationModel
                (
                    totalEntries, totalEntries, ProgressType.Import, LongRunningProcessType.APcashbookAccpacPosting, context.UserId
                ));
            }
        }
    }
}
