namespace Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels
{
    /// <summary>
    /// Represents a detailed entry for an Accounts Payable (AP) invoice batch, including account information, description, and amount.
    /// </summary>
    public class ApInvoiceBatchDetailEntryModel
    {
        /// <summary>
        /// Gets or sets the account identifier associated with the AP invoice batch detail.
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// Gets or sets the description for the AP invoice batch detail entry.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the amount for the AP invoice batch detail entry.
        /// </summary>
        public decimal Amount { get; set; }
    }

}
