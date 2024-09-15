using System.ComponentModel.DataAnnotations;
using Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels;

namespace Wrapper.API.DTO.APDTOs.Models
{
    /// <summary>
    /// Represents the header entry model for an Accounts Payable invoice batch.
    /// </summary>
    public class RequestApInvoiceBatchHeaderEntryModel
    {
        /// <summary>
        /// Gets or sets the vendor identifier.
        /// </summary>
        /// <example>V12345</example>
        [Required]
        public string VendorId { get; set; }

        /// <summary>
        /// Gets or sets the invoice number.
        /// </summary>
        /// <example>INV20230914</example>
        [Required]
        public string InvoiceNo { get; set; }

        /// <summary>
        /// Gets or sets the date of the invoice.
        /// </summary>
        /// <example>2024-05-14</example>
        [Required]
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Gets or sets the description of the invoice.
        /// </summary>
        /// <example>Office Supplies</example>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the total amount of the invoice.
        /// </summary>
        /// <example>1000.50</example>
        [Required]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the credit code associated with the invoice.
        /// </summary>
        /// <example>CRD001</example>
        [Required]
        public string CreditCode { get; set; }

        /// <summary>
        /// Gets or sets the type of the transaction for the invoice.
        /// </summary>
        [Required]
        public ApInvoiceBatchTransactionType TransactionType { get; set; }

        /// <summary>
        /// Gets or sets the details of the invoice, typically a list of line items.
        /// </summary>
        [Required]
        public List<RequestApInvoiceBatchDetailEntryModel> Details { get; set; }
    }

}
