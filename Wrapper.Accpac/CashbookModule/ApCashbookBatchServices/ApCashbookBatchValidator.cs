using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels;
using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels.ApCashbookBatchModels;
using Wrapper.Models.Common;
using Wrapper.Services;
using Wrapper.Services.Accpac.APModule.APSetupServices;
using Wrapper.Services.Accpac.CashbookModule;
using Wrapper.Services.Accpac.CashbookModule.ApCashbookBatchServices;
using Wrapper.Services.Accpac.CommonServicesModule;
using Wrapper.Services.Accpac.GLModule.GLSetupServices;

namespace Wrapper.Accpac.CashbookModule.ApCashbookBatchServices
{
    public class ApCashbookBatchValidator: IApCashbookBatchValidator
    {
        private readonly ICashbookSetupValidor cashbookSetupValidor;
        private readonly ICommonServicesValidator commonServicesValidator;
        private readonly IGLSetupValidator gLSetupValidator;
        private readonly IApModuleSetupValidator apModuleSetupValidator;

        public ApCashbookBatchValidator(
                ICashbookSetupValidor cashbookSetupValidor,
                ICommonServicesValidator commonServicesValidator,
                IGLSetupValidator gLSetupValidator,
                IApModuleSetupValidator apModuleSetupValidator
            ) 
        {
            this.cashbookSetupValidor = cashbookSetupValidor;
            this.commonServicesValidator = commonServicesValidator;
            this.gLSetupValidator = gLSetupValidator;
            this.apModuleSetupValidator = apModuleSetupValidator;
        }

        public async Task ValidateBatchAsync(IOperationContext context, ApCashbookBatchEntryModel model)
        {
            var validator = new Validator();
            await ValidateBatchAsync(context, model, validator);

            int lineNo = 1;
            foreach (ApCashbookBatchHeaderEntryModel header in model.Headers)
            {
                await commonServicesValidator.ValidateCurrencyExistsAsync(context, validator, header.Currency, lineNo);
                await apModuleSetupValidator.ValidateVendorExistsAndIsActiveAsync(context, validator, header.MiscCode, lineNo);
                await commonServicesValidator.ValidateOptionalFieldValueExistsAsync(context, validator, header.CreditCode, lineNo);
                
                if (header.ExchangeRate <= 0)
                {
                    validator.WriteLineError(lineNo, $"Exchange rate must be greater than zero");
                }

                commonServicesValidator.ValidateFiscalDate(context, validator, header.EntryDate, lineNo, header.ReferenceNo);

                lineNo++;

                int detailLine = 0;
                foreach (CashbookBatchEntryDetailModel detail in header.Details)
                {

                    detailLine++;

                    await cashbookSetupValidor.ValidateSourceCodeExistsAsync(context, validator, detail.SourceCode, detailLine);
                    await gLSetupValidator.ValidateAccountExistsAndIsActiveAsync(context, validator, detail.AccountCode, detailLine);

                    if (!detail.DebitAmount.HasValue && !detail.CreditAmount.HasValue)
                    {
                        validator.WriteLineError(detailLine, $"{header.ReferenceNo}: Either Debit or Credit amount must be provided");
                        continue;
                    }

                    if (detail.DebitAmount.HasValue && detail.DebitAmount < 0)
                    {
                        validator.WriteLineError(detailLine, $"{header.ReferenceNo}: Debit amount must be greater than zero");
                    }

                    if (detail.CreditAmount.HasValue && detail.CreditAmount < 0)
                    {
                        validator.WriteLineError(detailLine, $"{header.ReferenceNo}: Credit amount must be greater than zero");
                    }
                }
            }

            validator.Validate();

        }

        private async Task ValidateBatchAsync(IOperationContext context, ApCashbookBatchEntryModel model, Validator validator)
        {
            await cashbookSetupValidor.ValidateBankExistsAndIsActiveAsync(context, validator, model.BankCode, lineNo: null);

            if (model.Headers == null || !model.Headers.Any())
            {
                validator.WriteErrorAndValidate("Batch must have at least one header", lineNo: null);
            }

            if (model.Headers.Any(x => x.Details == null || !x.Details.Any()))
            {
                validator.WriteErrorAndValidate("All batch headers must have at least one detail", lineNo: null);
            }
        }
    }
}
