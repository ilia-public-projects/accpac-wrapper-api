using Wrapper.Models.Accpac.CommonServicesModels;
using Wrapper.Models.Common;

namespace Wrapper.Services.Accpac.CommonServicesModule
{
    /// <summary>
    /// Provides validation for accpac common services entities
    /// </summary>
    public interface ICommonServicesValidator
    {
        /// <summary>
        /// Validates that a currency exists in the system.
        /// If the currency does not exist, an error is written and validation fails.
        /// </summary>
        /// <param name="context">The operation context containing information about the current operation.</param>
        /// <param name="validator">The validator responsible for handling validation errors.</param>
        /// <param name="currencyCode">The currency code to be validated.</param>
        /// <param name="lineNo">The optional line number associated with the validation for logging purposes.</param>
        /// <returns>A task representing the asynchronous validation operation.</returns>
        Task ValidateCurrencyExistsAsync(IOperationContext context, Validator validator, string currencyCode, int? lineNo);

        /// <summary>
        /// Validates that the fiscal date is within an open fiscal period.
        /// If the fiscal period is not defined or not open, an error is written and validation fails.
        /// </summary>
        /// <param name="context">The operation context containing information about the current operation.</param>
        /// <param name="validator">The validator responsible for handling validation errors.</param>
        /// <param name="entryDate">The date to be validated against the fiscal calendar.</param>
        /// <param name="lineNo">The optional line number associated with the validation for logging purposes.</param>
        /// <param name="details">Additional details to include in the validation message, if necessary.</param>
        void ValidateFiscalDate(IOperationContext context, Validator validator, DateTime entryDate, int? lineNo, string details);

        /// <summary>
        /// Validates that an optional field value exists in the system.
        /// If the optional field value does not exist, an error is written and validation fails.
        /// </summary>
        /// <param name="context">The operation context containing information about the current operation.</param>
        /// <param name="validator">The validator responsible for handling validation errors.</param>
        /// <param name="optValue">The optional field value to be validated.</param>
        /// <param name="lineNo">The optional line number associated with the validation for logging purposes.</param>
        /// <returns>The <see cref="OptionalFieldValueModel"/> if it exists; otherwise, an error is written and validation fails.</returns>
        Task<OptionalFieldValueModel> ValidateOptionalFieldValueExistsAsync(IOperationContext context, Validator validator, string optValue, int? lineNo);
    }
}
