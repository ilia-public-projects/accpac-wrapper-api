using Microsoft.Extensions.Logging;
using Wrapper.Accpac.BatchInsertTemplate;
using Wrapper.Models;
using Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels;
using Wrapper.Models.Common;
using Wrapper.Services;
using Wrapper.Services.Accpac.APModule.APInvoiceBatchServices;
using Wrapper.Services.Common;

namespace Wrapper.Accpac.APModule.APInvoiceBatchServices
{
    /// <summary>
    /// Processor for inserting AP Invoice batches into the Accpac system,
    /// extending the BatchInsertProcessor for AP Invoice batches.
    /// </summary>
    public class ApInvoiceBatchInsertProcessor : BatchInsertProcessor<ApInvoiceBatchHeader>
    {
        private readonly IApInvoiceBatchViewComposer viewComposer;
        private readonly IAPInvoiceBatchValidator validator;
        private readonly IApInvoiceBatchViewProcessor viewProcessor;


        public ApInvoiceBatchInsertProcessor(
            ILogger<ApInvoiceBatchInsertProcessor> logger,
            INotificationMessenger notificationMessenger,
            IApInvoiceBatchViewComposer viewComposer,
            IAPInvoiceBatchValidator validator,
            IApInvoiceBatchViewProcessor viewProcessor
        ) : base(logger, notificationMessenger, LongRunningProcessType.APinvoiceAccpacPosting)
        {
            this.viewComposer = viewComposer;
            this.validator = validator;
            this.viewProcessor = viewProcessor;
        }

        /// <summary>
        /// Performs the batch insert operation, creating the batch, headers, and lines,
        /// and triggering progress notifications during the process.
        /// </summary>
        /// <param name="context">The operation context containing user and session data.</param>
        /// <param name="batchModel">The AP Invoice batch model containing headers and details.</param>
        /// <param name="trigger">Trigger for notifying progress during header processing.</param>
        protected override async Task PerformBatchInsertAsync(IOperationContext context, IBatchModel<ApInvoiceBatchHeader> batchModel, BatchHeaderProgressTrigger trigger)
        {
            int currentEntry = 0;

            var model = batchModel as ApInvoiceBatch;

            // Build the Accpac view for the batch
            ApInvoiceBatchView view = viewComposer.BuildBatchView(context);

            // Create the batch in Accpac
            viewProcessor.CreateBatch(context, view, model.BatchName, model.BatchDate);

            // Process each header and its associated lines
            foreach (ApInvoiceBatchHeader header in model.Headers)
            {
                // Notify progress for each processed header
                currentEntry++;
                await trigger(currentEntry);

                // Perform header and line operations in the view processor
                viewProcessor.CreateHeader(context, view, header);
                viewProcessor.CreateHeaderOptionalField(context, view, header);
                viewProcessor.AddLinesToHeader(context, view, header);
                viewProcessor.InsertHeader(context, view);
            }

            // Update the batch after all headers and lines have been processed
            viewProcessor.UpdateBatch(context, view);
        }

        /// <summary>
        /// Validates the AP Invoice batch before processing it.
        /// </summary>
        /// <param name="context">The operation context containing user and session data.</param>
        /// <param name="batchModel">The batch model to be validated.</param>
        protected override async Task ValidateBatchAsync(IOperationContext context, IBatchModel<ApInvoiceBatchHeader> batchModel)
        {
            var apBatch = batchModel as ApInvoiceBatch;
            await validator.ValidateCreateInvoiceBatchAsync(context, apBatch);
        }
    }

}
