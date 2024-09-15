namespace Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels
{
    /// <summary>
    /// Represents a detailed entry for a cashbook batch, including source code, account details, and debit/credit amounts.
    /// </summary>
    public class CashbookBatchEntryDetailModel
    {
        /// <summary>
        /// Gets or sets the source code for the cashbook batch entry.
        /// </summary>
        public string SourceCode { get; set; }

        /// <summary>
        /// Gets or sets the description of the cashbook batch entry detail.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the account code associated with the cashbook batch entry detail.
        /// </summary>
        public string AccountCode { get; set; }

        /// <summary>
        /// Gets or sets the debit amount for the cashbook batch entry.
        /// This value is nullable.
        /// </summary>
        public decimal? DebitAmount { get; set; }

        /// <summary>
        /// Gets or sets the credit amount for the cashbook batch entry.
        /// This value is nullable.
        /// </summary>
        public decimal? CreditAmount { get; set; }
    }

}
