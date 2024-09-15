using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels.NominalCashbookBatchModels;

namespace Wrapper.Services.Accpac.CashbookModule.NominalCashbookBatch
{
    /// <summary>
    /// Validates nominal cashbook batch entries, including batch headers, details, and associated data.
    /// </summary>
    public interface INominalCashbookBatchValidator
    {
        /// <summary>
        /// Validates the batch entries, including headers and details, and performs various checks on the batch data.
        /// </summary>
        /// <param name="context">The operation context containing information about the current operation.</param>
        /// <param name="model">The nominal cashbook batch entry model to validate.</param>
        /// <returns>A task representing the asynchronous operation.</returns>

        Task ValidateBatchAsync(IOperationContext context, NominalCashbookBatchEntryModel model);
    }
}
