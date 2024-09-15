namespace Wrapper.API.DTO.APDTOs.Models
{
    /// <summary>
    /// Represents an Accounts Payable invoice batch identifier in the response.
    /// </summary>
    public class ResponseApInvoiceBatchIdentifier
    {
        /// <summary>
        /// Gets or sets the batch number.
        /// </summary>
        /// <example>1001</example>
        public int BatchNo { get; set; }

        /// <summary>
        /// Gets or sets the description of the batch.
        /// </summary>
        /// <example>Batch for May 2024 Invoices</example>
        public string BatchDescription { get; set; }
    }

}
