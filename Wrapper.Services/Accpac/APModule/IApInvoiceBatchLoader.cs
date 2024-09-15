using Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels;

namespace Wrapper.Services.Accpac.APModule
{
    /// <summary>
    /// Loads information about Accounts Payable invoice batches from accpac.
    /// </summary>
    public interface IApInvoiceBatchLoader
    {
        /// <summary>
        /// Retrieves a list of active Accounts Payable invoice batch identifiers from the database.
        /// </summary>
        /// <param name="context">The operation context providing access to the database connection.</param>
        /// <returns>A list of <see cref="ApInvoiceBatchIdentifier"/> representing the active invoice batches.</returns>

        Task<List<ApInvoiceBatchIdentifier>> GetActiveBatchIdentifiersAsync(IOperationContext context);
    }
}
