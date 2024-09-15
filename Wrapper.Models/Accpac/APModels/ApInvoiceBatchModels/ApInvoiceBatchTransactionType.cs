namespace Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels
{
    /// <summary>
    /// Represents the transaction types for an Accounts Payable (AP) invoice batch.
    /// </summary>
    public enum ApInvoiceBatchTransactionType
    {
        /// <summary>
        /// Represents an invoice transaction type.
        /// </summary>
        Invoice = 1,

        /// <summary>
        /// Represents a debit note transaction type.
        /// </summary>
        DebitNote = 2,

        /// <summary>
        /// Represents a credit note transaction type.
        /// </summary>
        CreditNote = 3
    }

}
