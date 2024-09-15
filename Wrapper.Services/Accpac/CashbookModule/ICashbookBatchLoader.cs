using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels;

namespace Wrapper.Services.Accpac.CashbookModule
{
    /// <summary>
    /// Provides functionality for loading cashbook batch identifiers from the database.
    /// </summary>
    public interface ICashbookBatchLoader
    {
        /// <summary>
        /// Retrieves a list of active cashbook batch identifiers from the database asynchronously.
        /// </summary>
        /// <param name="context">The operation context containing information about the current operation.</param>
        /// <returns>A list of <see cref="CashbookBatchIdentifier"/> objects representing the active cashbook batches.</returns>
        Task<List<CashbookBatchIdentifier>> GetActiveBatchIdentifiersAsync(IOperationContext context);
    }
}
