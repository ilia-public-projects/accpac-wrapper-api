namespace Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels.NominalCashbookBatchModels
{
    /// <summary>
    /// Represents a nominal cashbook batch entry, extending the basic cashbook batch entry with a list of headers.
    /// </summary>
    public class NominalCashbookBatch : CashbookBatchBase, IBatchModel<CashbookBatchHeader>
    {
        /// <summary>
        /// Gets or sets the list of headers associated with the nominal cashbook batch entry.
        /// </summary>
        public List<CashbookBatchHeader> Headers { get; set; }
    }

}
