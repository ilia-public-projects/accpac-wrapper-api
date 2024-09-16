namespace Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels
{
    /// <summary>
    /// Represents an entry for an Accounts Payable (AP) invoice batch, including the batch name, batch date, and associated headers.
    /// </summary>
    public class ApInvoiceBatch : IBatchModel<ApInvoiceBatchHeader>
    {
        /// <summary>
        /// Gets or sets the name of the AP invoice batch.
        /// </summary>
        public string BatchName { get; set; }

        /// <summary>
        /// Gets or sets the date of the AP invoice batch.
        /// </summary>
        public DateTime BatchDate { get; set; }

        /// <summary>
        /// Gets or sets the list of headers associated with the AP invoice batch entry.
        /// </summary>
        public List<ApInvoiceBatchHeader> Headers { get; set; }
    }

}
