using Wrapper.Models.Accpac.APModels.APSetupModels;

namespace Wrapper.Services.Accpac.APModule.APSetupServices
{
    /// <summary>
    /// Loads vendor information from the accpac.
    /// </summary>
    public interface IVendorLoader
    {
        /// <summary>
        /// Retrieves a vendor from the database based on the provided vendor code.
        /// </summary>
        /// <param name="context">The operation context providing access to the database connection.</param>
        /// <param name="code">The vendor code for which the vendor information is to be retrieved.</param>
        /// <returns>A <see cref="VendorModel"/> representing the vendor information, or <c>null</c> if no vendor is found.</returns>
        Task<VendorModel> GetVendorByCodeAsync(IOperationContext context, string code);
    }
}
