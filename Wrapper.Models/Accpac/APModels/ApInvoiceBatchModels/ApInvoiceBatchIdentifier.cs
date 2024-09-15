namespace Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels
{
    /// <summary>
    /// Represents an identifier for an Accounts Payable (AP) invoice batch, including the batch number and a description of the batch.
    /// </summary>
    public class ApInvoiceBatchIdentifier
    {
        /// <summary>
        /// Gets or sets the batch number, which uniquely identifies the AP invoice batch.
        /// </summary>
        public int BatchNo { get; set; }

        /// <summary>
        /// Gets or sets the description of the AP invoice batch.
        /// </summary>
        public string BatchDescription { get; set; }
    }

}
