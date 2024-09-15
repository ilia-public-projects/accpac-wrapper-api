using Wrapper.Models.Common;

namespace Wrapper.Services.Accpac.GLModule.GLSetupServices
{
    /// <summary>
    /// Provides validation functionality for General Ledger (GL) setup
    /// </summary>
    public interface IGLSetupValidator
    {
        /// <summary>
        /// Validates that a GL account exists and is active in the system.
        /// If the account does not exist or is not active, an error is written and validation fails.
        /// </summary>
        /// <param name="context">The operation context containing information about the current operation.</param>
        /// <param name="validator">The validator responsible for handling validation errors.</param>
        /// <param name="accountCode">The GL account code to be validated.</param>
        /// <param name="lineNo">The optional line number associated with the validation for logging purposes.</param>
        /// <returns>A task representing the asynchronous validation operation.</returns>
        Task ValidateAccountExistsAndIsActiveAsync(IOperationContext context, Validator validator, string accountCode, int? lineNo);
    }
}
