namespace Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels.ApCashbookBatchModels
{
    /// <summary>
    /// Represents an Accounts Payable (AP) cashbook batch entry, extending the basic cashbook batch entry with a list of AP-specific headers.
    /// </summary>
    public class ApCashbookBatchEntryModel : CashbookBatchEntryModel
    {
        /// <summary>
        /// Gets or sets the list of AP-specific headers associated with the cashbook batch entry.
        /// </summary>
        public List<ApCashbookBatchHeaderEntryModel> Headers { get; set; }
    }

}
