namespace Wrapper.API.DTO.CashbookDTOs.Models
{
    /// <summary>
    /// Represents an identifier for a cashbook batch, including the batch number and a description of the batch.
    /// </summary>
    public class ResponseCashbookBatchIdentifier
    {
        /// <summary>
        /// Gets or sets the batch number.
        /// </summary>
        /// <example>CB20240914</example>
        public string BatchNo { get; set; }

        /// <summary>
        /// Gets or sets the description of the batch.
        /// </summary>
        /// <example>Cashbook batch for September 2024 transactions</example>
        public string BatchDescription { get; set; }
    }

}
