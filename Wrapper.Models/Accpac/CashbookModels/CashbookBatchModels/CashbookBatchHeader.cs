namespace Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels
{
    /// <summary>
    /// Represents the header entry for a cashbook batch, including currency, entry details, and associated batch details.
    /// </summary>
    public class CashbookBatchHeader
    {
        /// <summary>
        /// Gets or sets the currency of the cashbook batch.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the entry date for the cashbook batch.
        /// </summary>
        public DateTime EntryDate { get; set; }

        /// <summary>
        /// Gets or sets the exchange rate used for the cashbook batch.
        /// </summary>
        public decimal ExchangeRate { get; set; }

        /// <summary>
        /// Gets or sets the reference number for the cashbook batch.
        /// </summary>
        public string ReferenceNo { get; set; }

        /// <summary>
        /// Gets or sets the description of the cashbook batch.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the list of details associated with the cashbook batch entry.
        /// </summary>
        public List<CashbookBatchDetail> Details { get; set; }
    }

}
