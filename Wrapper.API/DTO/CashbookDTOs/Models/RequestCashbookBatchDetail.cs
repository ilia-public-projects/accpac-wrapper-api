using System.ComponentModel.DataAnnotations;

namespace Wrapper.API.DTO.CashbookDTOs.Models
{
    /// <summary>
    /// Represents the details of a cashbook batch entry, including source code, account code, 
    /// debit and credit amounts, and a description of the transaction.
    /// </summary>
    public class RequestCashbookBatchDetail
    {
        /// <summary>
        /// Gets or sets the source code that identifies the origin of the transaction.
        /// </summary>
        /// <example>SRC001</example>
        [Required]
        public string SourceCode { get; set; }

        /// <summary>
        /// Gets or sets the description of the transaction or batch detail.
        /// </summary>
        /// <example>Payment for vendor invoice</example>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the account code associated with the transaction.
        /// </summary>
        /// <example>ACC12345</example>
        [Required]
        public string AccountCode { get; set; }

        /// <summary>
        /// Gets or sets the debit amount for the transaction, if applicable.
        /// </summary>
        /// <example>1500.50</example>
        public decimal? DebitAmount { get; set; }

        /// <summary>
        /// Gets or sets the credit amount for the transaction, if applicable.
        /// </summary>
        /// <example>0.00</example>
        public decimal? CreditAmount { get; set; }
    }

}
