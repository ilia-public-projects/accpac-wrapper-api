using AccpacCOMAPI;
using Microsoft.Extensions.Logging;
using Wrapper.Accpac.BatchInsertTemplate;
using Wrapper.Models;
using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels;
using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels.ApCashbookBatchModels;
using Wrapper.Models.Common;
using Wrapper.Services;
using Wrapper.Services.Accpac.CashbookModule;
using Wrapper.Services.Accpac.CashbookModule.ApCashbookBatchServices;
using Wrapper.Services.Common;

namespace Wrapper.Accpac.CashbookModule.ApCashbookBatchServices
{
    /// <summary>
    /// Processor class responsible for inserting AP Cashbook batches into the Accpac system,
    /// extending the BatchInsertProcessor for handling the batch insert logic for AP Cashbook batches.
    /// </summary>
    public class ApCashbookBatchInsertProcessor : BatchInsertProcessor<ApCashbookBatchHeader>
    {

        private readonly ICashbookBatchViewComposer viewComposer;
        private readonly IApCashbookBatchValidator validator;


        public ApCashbookBatchInsertProcessor(
            ILogger<ApCashbookBatchInsertProcessor> logger,
            INotificationMessenger notificationMessenger,
            ICashbookBatchViewComposer viewComposer,
            IApCashbookBatchValidator validator
        ) : base(logger, notificationMessenger, LongRunningProcessType.APcashbookAccpacPosting)
        {
            this.viewComposer = viewComposer;
            this.validator = validator;
        }

        /// <summary>
        /// Performs the batch insert operation, creating the batch, headers, details, and sub-details,
        /// and triggering progress notifications during the process.
        /// </summary>
        /// <param name="context">The operation context containing user and session data.</param>
        /// <param name="batchModel">The batch model containing headers and details for AP Cashbook entries.</param>
        /// <param name="trigger">Trigger for notifying progress during header processing.</param>
        protected override async Task PerformBatchInsertAsync(IOperationContext context, IBatchModel<ApCashbookBatchHeader> batchModel, BatchHeaderProgressTrigger trigger)
        {
            int currentEntry = 0;

            // Cast the batch model to ApCashbookBatch to access its properties
            var model = batchModel as ApCashbookBatch;

            // Build the view for the batch using the view composer
            CashbookBatchView view = viewComposer.BuildBatchView(context);

            // Create a new batch record and set the bank code and batch name
            view.BatchView.RecordCreate(tagViewRecordCreateEnum.VIEW_RECORD_CREATE_INSERT);
            view.BatchView.Fields.FieldByName["BANKCODE"].PutWithoutVerification(model.BankCode);
            view.BatchView.Fields.FieldByName["TEXTDESC"].PutWithoutVerification(model.BatchName);
            view.BatchView.Update(); // Save the batch

            // Loop through each header in the batch
            foreach (ApCashbookBatchHeader header in model.Headers)
            {
                // Notify progress for the current header being processed
                currentEntry++;
                await trigger(currentEntry);

                // Create a new header record and set required fields
                view.HeaderView.RecordCreate(tagViewRecordCreateEnum.VIEW_RECORD_CREATE_INSERT);
                view.HeaderView.Fields.FieldByName["DATE"].PutWithoutVerification(header.EntryDate);
                view.HeaderView.Fields.FieldByName["ENTRYTYPE"].PutWithoutVerification("1"); // Set entry type to AP
                view.HeaderView.Fields.FieldByName["MISCCODE"].PutWithoutVerification(header.MiscCode);

                // Set currency and exchange rate fields
                view.HeaderView.Fields.FieldByName["BT2GLCURSR"].PutWithoutVerification(header.Currency);
                view.HeaderView.Fields.FieldByName["BT2GLRATE"].PutWithoutVerification(header.ExchangeRate);
                view.HeaderView.Fields.FieldByName["BK2GLRATE"].PutWithoutVerification(header.ExchangeRate);

                // Set reference number and description for the header
                view.HeaderView.Fields.FieldByName["REFERENCE"].PutWithoutVerification(header.ReferenceNo);
                view.HeaderView.Fields.FieldByName["TEXTDESC"].PutWithoutVerification(header.Description);

                // Create header optional fields
                view.HeaderOptFieldsView.RecordCreate(tagViewRecordCreateEnum.VIEW_RECORD_CREATE_INSERT);
                view.HeaderOptFieldsView.Fields.FieldByName["OPTFIELD"].PutWithoutVerification("CREDITCODE");
                view.HeaderOptFieldsView.Fields.FieldByName["SWSET"].PutWithoutVerification("1");
                view.HeaderOptFieldsView.Fields.FieldByName["VALIFTEXT"].PutWithoutVerification(header.CreditCode);
                view.HeaderOptFieldsView.Insert(); // Insert optional fields

                // Loop through each detail in the header and add it to the batch
                foreach (CashbookBatchDetail detail in header.Details)
                {
                    // Create a new detail record and set fields
                    view.DetailView.RecordCreate(tagViewRecordCreateEnum.VIEW_RECORD_CREATE_NOINSERT);
                    view.DetailView.Fields.FieldByName["SRCECODE"].PutWithoutVerification(detail.SourceCode);
                    view.DetailView.Fields.FieldByName["TEXTDESC"].PutWithoutVerification(detail.Description);
                    view.DetailView.Fields.FieldByName["ALLOCMODE"].PutWithoutVerification("1"); // Prepayment allocation mode

                    // Set debit or credit amount depending on the detail
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

                    view.DetailView.Insert(); // Insert the detail record

                    // Create sub-detail records for the detail
                    view.SubDetailView.RecordCreate(tagViewRecordCreateEnum.VIEW_RECORD_CREATE_INSERT);
                    view.SubDetailView.Fields.FieldByName["DOCNUMBER"].PutWithoutVerification("0");
                    view.SubDetailView.Fields.FieldByName["PAYNUMBER"].PutWithoutVerification(1);
                    view.SubDetailView.Fields.FieldByName["DOCTYPE"].PutWithoutVerification(0);
                    view.SubDetailView.Fields.FieldByName["APPLAMOUNT"].set_Value(amount); // Set applied amount
                    view.SubDetailView.Fields.FieldByName["IDCUST"].PutWithoutVerification(header.MiscCode);
                    view.SubDetailView.Fields.FieldByName["ENTRYTYPE"].PutWithoutVerification("1");

                    view.SubDetailView.Insert(); // Insert sub-detail record

                    // Update the detail record and clear it for the next iteration
                    view.DetailView.Update();
                    view.DetailView.RecordClear();
                    view.SubDetailView.RecordClear();
                }

                // Insert and clear the header record for the next header
                view.HeaderView.Insert();
                view.HeaderOptFieldsView.RecordClear();
                view.HeaderView.RecordClear();
            }
        }

        /// <summary>
        /// Validates the AP Cashbook batch before processing it.
        /// </summary>
        /// <param name="context">The operation context containing user and session data.</param>
        /// <param name="batchModel">The batch model to be validated.</param>
        protected override async Task ValidateBatchAsync(IOperationContext context, IBatchModel<ApCashbookBatchHeader> batchModel)
        {
            // Cast the batch model to ApCashbookBatch to access its properties
            var batch = batchModel as ApCashbookBatch;

            // Perform validation using the validator service
            await validator.ValidateBatchAsync(context, batch);
        }
    }

}
