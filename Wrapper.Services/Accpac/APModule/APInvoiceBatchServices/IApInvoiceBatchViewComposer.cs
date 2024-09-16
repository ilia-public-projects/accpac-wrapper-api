using Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels;

namespace Wrapper.Services.Accpac.APModule.APInvoiceBatchServices
{
    /// <summary>
    /// Composes and provides access to the views required for handling Accounts Payable invoice batches.
    /// </summary>
    public interface IApInvoiceBatchViewComposer
    {
        /// <summary>
        /// Builds and composes the necessary views for an Accounts Payable invoice batch.
        /// </summary>
        /// <param name="context">The operation context providing access to the database link.</param>
        /// <returns>An instance of <see cref="ApInvoiceBatchView"/> containing the composed views.</returns>
        ApInvoiceBatchView BuildBatchView(IOperationContext context);
    }
}
