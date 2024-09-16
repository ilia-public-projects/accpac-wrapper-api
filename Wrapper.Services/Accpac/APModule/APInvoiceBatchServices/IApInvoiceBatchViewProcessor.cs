using Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels;

namespace Wrapper.Services.Accpac.APModule.APInvoiceBatchServices
{

    /// <summary>
    /// Interface for processing operations related to the AP Invoice Batch View.
    /// Defines methods to create batches, headers, lines, and handle batch updates within the Accpac system.
    /// </summary>
    public interface IApInvoiceBatchViewProcessor
    {
        /// <summary>
        /// Adds detail lines to the specified AP invoice header in the batch view.
        /// </summary>
        /// <param name="context">The operation context containing user and session data.</param>
        /// <param name="view">The view object representing the AP invoice batch.</param>
        /// <param name="header">The header model containing the detail lines to be added.</param>
        void AddLinesToHeader(IOperationContext context, ApInvoiceBatchView view, ApInvoiceBatchHeader header);

        /// <summary>
        /// Creates a new AP invoice batch in the Accpac system.
        /// </summary>
        /// <param name="context">The operation context containing user and session data.</param>
        /// <param name="view">The view object representing the AP invoice batch.</param>
        /// <param name="batchName">The name of the batch to be created.</param>
        /// <param name="batchDate">The date of the batch to be created.</param>
        void CreateBatch(IOperationContext context, ApInvoiceBatchView view, string batchName, DateTime batchDate);

        /// <summary>
        /// Creates a new header for the AP invoice batch in the Accpac system.s
        /// </summary>
        /// <param name="context">The operation context containing user and session data.</param>
        /// <param name="view">The view object representing the AP invoice batch.</param>
        /// <param name="header">The header model containing data for the AP invoice header.</param>
        void CreateHeader(IOperationContext context, ApInvoiceBatchView view, ApInvoiceBatchHeader header);

        /// <summary>
        /// Creates an optional field for the specified AP invoice header in the batch view.
        /// </summary>
        /// <param name="context">The operation context containing user and session data.</param>
        /// <param name="view">The view object representing the AP invoice batch.</param>
        /// <param name="header">The header model containing the optional field information.</param>
        void CreateHeaderOptionalField(IOperationContext context, ApInvoiceBatchView view, ApInvoiceBatchHeader header);

        /// <summary>
        /// Inserts the current AP invoice header into the Accpac system.
        /// </summary>
        /// <param name="context">The operation context containing user and session data.</param>
        /// <param name="view">The view object representing the AP invoice batch.</param>
        void InsertHeader(IOperationContext context, ApInvoiceBatchView view);

        /// <summary>
        /// Updates the AP invoice batch after processing all headers and lines, 
        /// committing any changes made to the batch in the Accpac system.
        /// </summary>
        /// <param name="context">The operation context containing user and session data.</param>
        /// <param name="view">The view object representing the AP invoice batch.</param>
        void UpdateBatch(IOperationContext context, ApInvoiceBatchView view);
    }

}
