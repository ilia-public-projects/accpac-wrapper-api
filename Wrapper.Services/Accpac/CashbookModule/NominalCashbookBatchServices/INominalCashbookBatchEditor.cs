using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels.NominalCashbookBatchModels;
using Wrapper.Models.Common.Exceptions;

namespace Wrapper.Services.Accpac.CashbookModule.NominalCashbookBatchServices
{
    /// <summary>
    /// Handles the creation and processing of nominal cashbook batches
    /// </summary>
    public interface INominalCashbookBatchEditor
    {
        /// <summary>
        /// Creates a nominal cashbook batch asynchronously, including validation, view updates, and database transactions.
        /// </summary>
        /// <param name="context">The operation context containing details about the current operation.</param>
        /// <param name="model">The nominal cashbook batch entry model to be created.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="CreateEntityException">Thrown when an error occurs during the creation process.</exception>
        Task CreateNominalCashbookBatchAsync(IOperationContext context, NominalCashbookBatch model);
    }
}
