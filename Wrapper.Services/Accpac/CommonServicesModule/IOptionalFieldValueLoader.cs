using Wrapper.Models.Accpac.APModels.APSetupModels;
using Wrapper.Models.Accpac.CommonServicesModels;

namespace Wrapper.Services.Accpac.CommonServicesModule
{
    /// <summary>
    /// Provides functionality for loading optional field values from accpac and verifying if optional fields are defined in specific AP locations.
    /// </summary>
    public interface IOptionalFieldValueLoader
    {
        /// <summary>
        /// Retrieves an optional field value by its value from the database asynchronously.
        /// </summary>
        /// <param name="context">The operation context containing information about the current operation.</param>
        /// <param name="code">The value of the optional field to be retrieved.</param>
        /// <returns>The <see cref="OptionalFieldValueModel"/> containing the optional field and its value, or <c>null</c> if not found.</returns>
        Task<OptionalFieldValueModel> GetOptionaFieldValueByValueAsync(IOperationContext context, string code);

        /// <summary>
        /// Checks if an optional field is defined in a specific Accounts Payable (AP) location asynchronously.
        /// </summary>
        /// <param name="context">The operation context containing information about the current operation.</param>
        /// <param name="optionalField">The optional field to be checked.</param>
        /// <param name="location">The AP location where the optional field is defined.</param>
        /// <returns><c>true</c> if the optional field is defined in the specified location; otherwise, <c>false</c>.</returns>
        Task<bool> OptionalFieldDefinedInApLocationAsync(IOperationContext context, string optionalField, ApOptionalFieldLocation location);
    }
}
