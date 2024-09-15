using Microsoft.Extensions.Logging;
using Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels;
using Wrapper.Models.Common;
using Wrapper.Models.Common.Exceptions;
using Wrapper.Services;
using Wrapper.Services.Accpac.APModule;
using Wrapper.Services.Accpac.APModule.APInvoiceBatchServices;
using Wrapper.Services.Common;
using Wrapper.Services.Utils;

namespace Wrapper.Accpac.APModule.APInvoiceBatchServices
{
    public class APInvoiceBatchEditor : IAPInvoiceBatchEditor
    {
        private readonly ILogger<APInvoiceBatchEditor> logger;
        private readonly IAPInvoiceBatchValidator apInvoiceBatchValidator;
        private readonly IApInvoiceBatchViewComposer viewComposer;
        private readonly INotificationMessenger messenger;
        private readonly IAPInvoiceBatchAccpacProcessor apInvoiceBatchAccpacProcessor;

        public APInvoiceBatchEditor(
                ILogger<APInvoiceBatchEditor> logger,
                IAPInvoiceBatchValidator apInvoiceBatchValidator,
                IApInvoiceBatchViewComposer viewComposer,
                INotificationMessenger messenger,
                IAPInvoiceBatchAccpacProcessor apInvoiceBatchAccpacProcessor
            )
        {
            this.logger = logger;
            this.apInvoiceBatchValidator = apInvoiceBatchValidator;
            this.viewComposer = viewComposer;
            this.messenger = messenger;
            this.apInvoiceBatchAccpacProcessor = apInvoiceBatchAccpacProcessor;
        }

        public async Task CreateBatchAsync(IOperationContext context, ApInvoiceBatchEntryModel model)
        {
            await apInvoiceBatchValidator.ValidateCreateInvoiceBatchAsync(context, model);

            int currentEntry = 0;
            int totalEntries = model.Headers.Count;

            context.AccpacDBLink.TransactionBegin(out int p);

            try
            {
                ApInvoiceBatchView view = viewComposer.BuildBatchView(context);

                apInvoiceBatchAccpacProcessor.CreateBatch(context, view, model.BatchName, model.BatchDate);

                foreach (ApInvoiceBatchHeaderEntryModel header in model.Headers)
                {
                    // notify progress
                    currentEntry++;
                    await messenger.NotifyProgressAsync(context, new NotificationModel
                    (
                        currentEntry, totalEntries, ProgressType.Import, LongRunningProcessType.APinvoiceAccpacPosting, context.UserId
                    ));

                    apInvoiceBatchAccpacProcessor.CreateHeader(context, view, header);
                    apInvoiceBatchAccpacProcessor.CreateHeaderOptionalField(context, view, header);
                    apInvoiceBatchAccpacProcessor.AddLinesToHeader(context, view, header);
                    apInvoiceBatchAccpacProcessor.InsertHeader(context, view);
                }

                apInvoiceBatchAccpacProcessor.UpdateBatch(context, view);

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
                    totalEntries, totalEntries, ProgressType.Import, LongRunningProcessType.APinvoiceAccpacPosting, context.UserId
                ));
            }
        }
    }
}
