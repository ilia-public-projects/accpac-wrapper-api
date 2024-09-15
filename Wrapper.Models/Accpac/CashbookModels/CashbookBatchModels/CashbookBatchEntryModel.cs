namespace Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels
{
    /// <summary>
    /// Represents an entry for a cashbook batch, including the batch name and bank code.
    /// </summary>
    public class CashbookBatchEntryModel
    {
        /// <summary>
        /// Gets or sets the name of the cashbook batch.
        /// </summary>
        public string BatchName { get; set; }

        /// <summary>
        /// Gets or sets the bank code associated with the cashbook batch.
        /// </summary>
        public string BankCode { get; set; }
    }

}
