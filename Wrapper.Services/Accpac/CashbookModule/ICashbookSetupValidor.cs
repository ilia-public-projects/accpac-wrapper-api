using Wrapper.Models.Common;

namespace Wrapper.Services.Accpac.CashbookModule
{
    /// <summary>
    /// Validates cashbook setup details
    /// </summary>
    public interface ICashbookSetupValidor
    {
        /// <summary>
        /// Validates that a bank exists and is active.
        /// </summary>
        /// <param name="context">The operation context containing information about the current operation.</param>
        /// <param name="validator">The validator used to record errors.</param>
        /// <param name="bankCode">The code of the bank to validate.</param>
        /// <param name="lineNo">Optional line number where the error occurred, if applicable.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task ValidateBankExistsAndIsActiveAsync(IOperationContext context, Validator validator, string bankCode, int? lineNo);

        /// <summary>
        /// Validates that a source code exists.
        /// </summary>
        /// <param name="context">The operation context containing information about the current operation.</param>
        /// <param name="validator">The validator used to record errors.</param>
        /// <param name="sourceCode">The source code to validate.</param>
        /// <param name="lineNo">Optional line number where the error occurred, if applicable.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task ValidateSourceCodeExistsAsync(IOperationContext context, Validator validator, string sourceCode, int? lineNo);
    }
}
