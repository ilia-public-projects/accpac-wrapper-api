namespace Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels.ApCashbookBatchModels
{
    /// <summary>
    /// Represents an Accounts Payable (AP) cashbook batch header entry, extending the basic cashbook batch header with additional AP-specific fields.
    /// </summary>
    public class ApCashbookBatchHeader : CashbookBatchHeader
    {
        /// <summary>
        /// Gets or sets the miscellaneous code associated with the AP cashbook batch header.
        /// </summary>
        public string MiscCode { get; set; }

        /// <summary>
        /// Gets or sets the credit code associated with the AP cashbook batch header.
        /// </summary>
        public string CreditCode { get; set; }
    }

}
