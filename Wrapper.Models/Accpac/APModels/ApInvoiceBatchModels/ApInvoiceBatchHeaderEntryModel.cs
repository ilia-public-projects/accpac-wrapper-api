namespace Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels
{
    /// <summary>
    /// Represents the header entry for an Accounts Payable (AP) invoice batch, 
    /// including vendor information, invoice details, and transaction type.
    /// </summary>
    public class ApInvoiceBatchHeaderEntryModel
    {
        /// <summary>
        /// Gets or sets the vendor identifier for the AP invoice.
        /// </summary>
        public string VendorId { get; set; }

        /// <summary>
        /// Gets or sets the invoice number for the AP invoice.
        /// </summary>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// Gets or sets the date of the AP invoice.
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Gets or sets the description of the AP invoice.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the total amount for the AP invoice.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the credit code associated with the AP invoice.
        /// </summary>
        public string CreditCode { get; set; }

        /// <summary>
        /// Gets or sets the transaction type for the AP invoice, 
        /// such as Invoice, Debit Note, or Credit Note.
        /// </summary>
        public ApInvoiceBatchTransactionType TransactionType { get; set; }

        /// <summary>
        /// Gets or sets the list of details associated with the AP invoice batch entry.
        /// </summary>
        public List<ApInvoiceBatchDetailEntryModel> Details { get; set; }
    }

}
