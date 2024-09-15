namespace Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels
{
    /// <summary>
    /// Represents an identifier for a cashbook batch, including the batch number and a description of the batch.
    /// </summary>
    public class CashbookBatchIdentifier
    {
        /// <summary>
        /// Gets or sets the batch number, which uniquely identifies the cashbook batch.
        /// </summary>
        public string BatchNo { get; set; }

        /// <summary>
        /// Gets or sets the description of the cashbook batch.
        /// </summary>
        public string BatchDescription { get; set; }
    }

}
