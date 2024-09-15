using Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels;
using Wrapper.Models.Accpac.APModels.APSetupModels;
using Wrapper.Models.Common;
using Wrapper.Services;
using Wrapper.Services.Accpac.APModule.APInvoiceBatchServices;
using Wrapper.Services.Accpac.APModule.APSetupServices;
using Wrapper.Services.Accpac.CommonServicesModule;
using Wrapper.Services.Accpac.GLModule.GLSetupServices;

namespace Wrapper.Accpac.APModule.APInvoiceBatchServices
{
    public class APInvoiceBatchValidator : IAPInvoiceBatchValidator
    {
        private readonly IGLSetupValidator gLSetupValidator;
        private readonly IApModuleSetupValidator apModuleSetupValidator;
        private readonly ICommonServicesValidator commonServicesValidator;
        private readonly IOptionalFieldValueLoader optionalFieldValueLoader;

        public APInvoiceBatchValidator(
                IGLSetupValidator gLSetupValidator,
                IApModuleSetupValidator apModuleSetupValidator,
                ICommonServicesValidator commonServicesValidator,
                IOptionalFieldValueLoader optionalFieldValueLoader
            )
        {
            this.gLSetupValidator = gLSetupValidator;
            this.apModuleSetupValidator = apModuleSetupValidator;
            this.commonServicesValidator = commonServicesValidator;
            this.optionalFieldValueLoader = optionalFieldValueLoader;
        }

        public async Task ValidateCreateInvoiceBatchAsync(IOperationContext context, ApInvoiceBatchEntryModel model)
        {
            var validator = new Validator();
            if (model.Headers == null || !model.Headers.Any())
            {
                validator.WriteErrorAndValidate("No invoice headers found.", lineNo: null);
            }

            if (model.Headers.Any(x => x.Details == null || !x.Details.Any()))
            {
                validator.WriteErrorAndValidate("Please make sure all headers have details.", lineNo: null);
            }

            commonServicesValidator.ValidateFiscalDate(context, validator, model.BatchDate, lineNo: null, "Batch date");

            await ValidateOptionalFieldsAsync(context, validator, model);

            int lineNo = 0;
            foreach (ApInvoiceBatchHeaderEntryModel header in model.Headers)
            {
                lineNo++;

                await apModuleSetupValidator.ValidateVendorExistsAndIsActiveAsync(context, validator, header.VendorId, lineNo);
                commonServicesValidator.ValidateFiscalDate(context, validator, header.InvoiceDate, lineNo, header.InvoiceNo);

                if (header.TotalAmount <= 0)
                {
                    validator.WriteErrorAndValidate("Total amount must be greater than zero.", lineNo);
                }

                // validate transaction type is valid enum
                if (!Enum.IsDefined(typeof(ApInvoiceBatchTransactionType), header.TransactionType))
                {
                    validator.WriteErrorAndValidate($"({header.InvoiceNo}). Specified transaction type does not exist.", lineNo);
                }

                int detailNo = 0;
                foreach (ApInvoiceBatchDetailEntryModel detail in header.Details)
                {
                    detailNo++;

                    await gLSetupValidator.ValidateAccountExistsAndIsActiveAsync(context, validator, detail.AccountId, detailNo);

                    if (detail.Amount <= 0)
                    {
                        validator.WriteErrorAndValidate("Amount must be greater than zero.", lineNo);
                    }
                }

            }

            validator.Validate();

        }

        private async Task ValidateOptionalFieldsAsync(IOperationContext context, Validator validator, ApInvoiceBatchEntryModel model)
        {
            IEnumerable<string> creditCodes = model.Headers.Select(x => x.CreditCode).Distinct().ToList();
            foreach (string creditCode in creditCodes)
            {
                var optionalField = await commonServicesValidator.ValidateOptionalFieldValueExistsAsync(context, validator, creditCode, lineNo: null);

                bool isDefinedForInvoices = await optionalFieldValueLoader.OptionalFieldDefinedInApLocationAsync(context, creditCode, ApOptionalFieldLocation.Invoices);
                if (!isDefinedForInvoices)
                {
                    validator.WriteErrorAndValidate($"Optional field value ({optionalField.OptionalField}) is not defined for invoices in Accounts Payable setup.", lineNo: null);
                }
            }
        }
    }
}
