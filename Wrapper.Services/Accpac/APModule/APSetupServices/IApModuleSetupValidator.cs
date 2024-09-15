using Wrapper.Models.Common;

namespace Wrapper.Services.Accpac.APModule.APSetupServices
{
    /// <summary>
    /// Validates AP module setup
    /// </summary>
    public interface IApModuleSetupValidator
    {
        /// <summary>
        /// Validates whether a vendor exists and is active.
        /// </summary>
        /// <param name="context">The operation context providing access to the necessary resources.</param>
        /// <param name="validator">The validator used to write validation errors and messages.</param>
        /// <param name="code">The code of the vendor to validate.</param>
        /// <param name="lineNo">The line number associated with the validation, or <c>null</c> if not applicable.</param>
        Task ValidateVendorExistsAndIsActiveAsync(IOperationContext context, Validator validator, string code, int? lineNo);
    }
}
