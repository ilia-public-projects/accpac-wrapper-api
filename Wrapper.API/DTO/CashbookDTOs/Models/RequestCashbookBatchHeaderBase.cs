using System.ComponentModel.DataAnnotations;

namespace Wrapper.API.DTO.CashbookDTOs.Models
{
    /// <summary>
    /// Represents the base class for a cashbook batch header, containing common properties such as currency,
    /// entry date, exchange rate, reference number, description, and associated batch details.
    /// </summary>
    public class RequestCashbookBatchHeaderBase
    {
        /// <summary>
        /// Gets or sets the currency for the cashbook batch.
        /// </summary>
        /// <example>USD</example>
        [Required]
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the entry date of the cashbook batch.
        /// </summary>
        /// <example>2024-05-14</example>
        [Required]
        public DateTime EntryDate { get; set; }

        /// <summary>
        /// Gets or sets the exchange rate used for the cashbook batch header.
        /// </summary>
        /// <example>1.23</example>
        [Required]
        public decimal ExchangeRate { get; set; }

        /// <summary>
        /// Gets or sets the reference number for the cashbook batch entry.
        /// </summary>
        /// <example>REF123456</example>
        [Required]
        public string ReferenceNo { get; set; }

        /// <summary>
        /// Gets or sets the description of the cashbook batch entry.
        /// </summary>
        /// <example>Payment for September invoices</example>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the list of details associated with this cashbook batch header.
        /// </summary>
        [Required]
        public List<RequestCashbookBatchDetail> Details { get; set; }
    }

}
