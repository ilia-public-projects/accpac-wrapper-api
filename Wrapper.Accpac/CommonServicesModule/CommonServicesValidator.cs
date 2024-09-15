using AccpacCOMAPI;
using Wrapper.Models.Accpac.CommonServicesModels;
using Wrapper.Models.Common;
using Wrapper.Services;
using Wrapper.Services.Accpac.CommonServicesModule;

namespace Wrapper.Accpac.CommonServicesModule
{
    public class CommonServicesValidator : ICommonServicesValidator
    {
        private readonly ICurrencyLoader currencyLoader;
        private readonly IOptionalFieldValueLoader optionalFieldValueLoader;

        public CommonServicesValidator(
                ICurrencyLoader currencyLoader,
                IOptionalFieldValueLoader optionalFieldValueLoader
            )
        {
            this.currencyLoader = currencyLoader;
            this.optionalFieldValueLoader = optionalFieldValueLoader;
        }

        public async Task ValidateCurrencyExistsAsync(IOperationContext context, Validator validator, string currencyCode, int? lineNo)
        {
            List<CurrencyModel> currencies = await currencyLoader.GetCurrenciesAsync(context);
            if (!currencies.Any(c => c.Code == currencyCode))
            {
                validator.WriteErrorAndValidate("Currency does not exist", lineNo);
            }
        }

        public async Task<OptionalFieldValueModel> ValidateOptionalFieldValueExistsAsync(IOperationContext context, Validator validator, string optValue, int? lineNo)
        {
            OptionalFieldValueModel optFieldValue = await optionalFieldValueLoader.GetOptionaFieldValueByValueAsync(context, optValue);
            if (optFieldValue == null)
            {
                validator.WriteErrorAndValidate($"Optional field value ({optValue}) does not exist", lineNo);
            }

            return optFieldValue;
        }

        public void ValidateFiscalDate(IOperationContext context, Validator validator, DateTime entryDate, int? lineNo, string details)
        {
            AccpacFiscalCalendar fiscalCalendar = context.AccpacDBLink.GetFiscalCalendar();
            bool periodExists = fiscalCalendar.GetPeriodFromDate(context.AccpacSessionModuleDefault.ProgramName, entryDate, out AccpacFiscalPeriod period);
            if (!periodExists)
            {
                string error = $"Fiscal period not defined for entry date ({entryDate:dd/MM/yyyy})";
                if (!string.IsNullOrEmpty(details))
                {
                    error += $" - {details}";
                }
                validator.WriteErrorAndValidate(error, lineNo);
            }

            if (period.boStatus == 0)
            { 
                string error = $"Fiscal period is not open for entry date ({entryDate:dd/MM/yyyy})";
                if (!string.IsNullOrEmpty(details))
                {
                    error += $" - {details}";
                }
                validator.WriteErrorAndValidate(error, lineNo);
            }
        }
    }
}
