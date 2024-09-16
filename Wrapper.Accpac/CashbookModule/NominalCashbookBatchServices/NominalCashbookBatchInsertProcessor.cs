using AccpacCOMAPI;
using Microsoft.Extensions.Logging;
using Wrapper.Accpac.BatchInsertTemplate;
using Wrapper.Models;
using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels;
using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels.NominalCashbookBatchModels;
using Wrapper.Models.Common;
using Wrapper.Services;
using Wrapper.Services.Accpac.CashbookModule;
using Wrapper.Services.Accpac.CashbookModule.NominalCashbookBatchServices;
using Wrapper.Services.Common;

namespace Wrapper.Accpac.CashbookModule.NominalCashbookBatchServices
{
    /// <summary>
    /// Processor class responsible for inserting nominal cashbook batches into the Accpac system,
    /// extending the BatchInsertProcessor for handling batch insert logic for nominal cashbook batches.
    /// </summary>
    public class NominalCashbookBatchInsertProcessor : BatchInsertProcessor<CashbookBatchHeader>
    {
        private readonly INominalCashbookBatchValidator validator;
        private readonly ICashbookBatchViewComposer viewComposer;

        public NominalCashbookBatchInsertProcessor(
            ILogger<NominalCashbookBatchInsertProcessor> logger,
            INotificationMessenger notificationMessenger,
            INominalCashbookBatchValidator validator,
            ICashbookBatchViewComposer viewComposer
        ) : base(logger, notificationMessenger, LongRunningProcessType.NominalcashbookAccpacPosting)
        {
            this.validator = validator;
            this.viewComposer = viewComposer;
        }

        /// <summary>
        /// Performs the batch insert operation, creating the batch, headers, details, and triggering 
        /// progress notifications during the process.
        /// </summary>
        /// <param name="context">The operation context containing user and session data.</param>
        /// <param name="batchModel">The batch model containing headers and details for nominal cashbook entries.</param>
        /// <param name="trigger">Trigger for notifying progress during header processing.</param>
        protected override async Task PerformBatchInsertAsync(IOperationContext context, IBatchModel<CashbookBatchHeader> batchModel, BatchHeaderProgressTrigger trigger)
        {
            // Cast the batch model to NominalCashbookBatch to access its properties
            var model = batchModel as NominalCashbookBatch;

            int currentEntry = 0;

            // Build the view for the batch using the view composer
            CashbookBatchView view = viewComposer.BuildBatchView(context);

            // Create a new batch record and set the bank code and batch description
            view.BatchView.RecordCreate(tagViewRecordCreateEnum.VIEW_RECORD_CREATE_INSERT);
            view.BatchView.Fields.FieldByName["BANKCODE"].set_Value(model.BankCode);
            view.BatchView.Fields.FieldByName["TEXTDESC"].PutWithoutVerification(model.BatchName);
            view.BatchView.Update(); // Save the batch

            // Loop through each header in the batch
            foreach (CashbookBatchHeader header in model.Headers)
            {
                // Notify progress for the current header being processed
                currentEntry++;
                await trigger(currentEntry);

                // Create a new header record and set required fields
                view.HeaderView.RecordCreate(tagViewRecordCreateEnum.VIEW_RECORD_CREATE_NOINSERT);
                view.HeaderView.Fields.FieldByName["DATE"].set_Value(header.EntryDate);

                // Set currency and exchange rate fields
                view.HeaderView.Fields.FieldByName["BT2GLCURSR"].set_Value(header.Currency);
                view.HeaderView.Fields.FieldByName["BT2GLRATE"].set_Value(header.ExchangeRate);
                view.HeaderView.Fields.FieldByName["BK2GLRATE"].set_Value(header.ExchangeRate);

                // Set reference number and description for the header
                view.HeaderView.Fields.FieldByName["REFERENCE"].PutWithoutVerification(header.ReferenceNo);
                view.HeaderView.Fields.FieldByName["TEXTDESC"].PutWithoutVerification(header.Description);
                view.HeaderView.Process(); // Process the header

                // Loop through each detail in the header and add it to the batch
                foreach (CashbookBatchDetail detail in header.Details)
                {
                    // Create a new detail record and set fields
                    view.DetailView.RecordCreate(tagViewRecordCreateEnum.VIEW_RECORD_CREATE_NOINSERT);
                    view.DetailView.Fields.FieldByName["SRCECODE"].set_Value(detail.SourceCode);
                    view.DetailView.Fields.FieldByName["TEXTDESC"].PutWithoutVerification(detail.Description);
                    view.DetailView.Fields.FieldByName["ACCTID"].set_Value(detail.AccountCode);

                    // Set debit or credit amount depending on the detail
                    if (detail.DebitAmount.HasValue)
                    {
                        view.DetailView.Fields.FieldByName["DEBITAMT"].set_Value(detail.DebitAmount.Value);
                    }

                    if (detail.CreditAmount.HasValue)
                    {
                        view.DetailView.Fields.FieldByName["CREDITAMT"].set_Value(detail.CreditAmount.Value);
                    }

                    view.DetailView.Insert(); // Insert the detail record

                    // Insert the header after the details are processed
                    view.HeaderView.Insert();
                    view.BatchView.Update(); // Save the batch

                    // Clear the detail record for the next iteration
                    view.DetailView.RecordClear();
                }

                // Clear the header record for the next iteration
                view.HeaderView.RecordClear();
            }
        }

        /// <summary>
        /// Validates the nominal cashbook batch before processing it.
        /// </summary>
        /// <param name="context">The operation context containing user and session data.</param>
        /// <param name="batchModel">The batch model to be validated.</param>
        protected override async Task ValidateBatchAsync(IOperationContext context, IBatchModel<CashbookBatchHeader> batchModel)
        {
            // Cast the batch model to NominalCashbookBatch to access its properties
            var batch = batchModel as NominalCashbookBatch;

            // Perform validation using the validator service
            await validator.ValidateBatchAsync(context, batch);
        }
    }

}
