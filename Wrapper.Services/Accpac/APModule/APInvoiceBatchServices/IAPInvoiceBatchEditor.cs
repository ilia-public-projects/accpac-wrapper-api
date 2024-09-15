using Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels;

namespace Wrapper.Services.Accpac.APModule.APInvoiceBatchServices
{
    /// <summary>
    /// Handles the creation of an Accounts Payable (AP) invoice batch.
    /// </summary>
    public interface IAPInvoiceBatchEditor
    {
        /// <summary>
        /// Creates an AP invoice batch asynchronously based on the provided model.
        /// </summary>
        /// <param name="context">The operation context containing the session and database link.</param>
        /// <param name="model">The model representing the AP invoice batch to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>

        Task CreateBatchAsync(IOperationContext context, ApInvoiceBatchEntryModel model);
    }
}
