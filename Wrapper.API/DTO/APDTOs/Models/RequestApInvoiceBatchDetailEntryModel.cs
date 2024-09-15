using System.ComponentModel.DataAnnotations;

namespace Wrapper.API.DTO.APDTOs.Models
{
    /// <summary>
    /// Represents the detail entry model for an Accounts Payable invoice batch.
    /// This typically includes the individual line items or transaction details for an invoice.
    /// </summary>
    public class RequestApInvoiceBatchDetailEntryModel
    {
        /// <summary>
        /// Gets or sets the account identifier related to this detail entry.
        /// </summary>
        /// <example>ACC12345</example>
        [Required]
        public string AccountId { get; set; }

        /// <summary>
        /// Gets or sets the description for the specific line item or transaction detail.
        /// </summary>
        /// <example>Consulting Services</example>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the amount associated with this line item or detail.
        /// </summary>
        /// <example>500.75</example>
        [Required]
        public decimal Amount { get; set; }
    }

}
