using System.ComponentModel.DataAnnotations;

namespace Wrapper.API.DTO.CashbookDTOs.Models
{
    /// <summary>
    /// Represents an Accounts Payable cashbook batch header, inheriting common properties 
    /// from <see cref="RequestCashbookBatchHeaderBase"/> and adding specific fields for miscellaneous and credit codes.
    /// </summary>
    public class RequestApCashbookBatchHeader : RequestCashbookBatchHeaderBase
    {
        /// <summary>
        /// Gets or sets the miscellaneous code associated with the cashbook batch header.
        /// </summary>
        /// <example>MISC001</example>
        [Required]
        public string MiscCode { get; set; }

        /// <summary>
        /// Gets or sets the credit code associated with the cashbook batch header.
        /// </summary>
        /// <example>CREDIT001</example>
        [Required]
        public string CreditCode { get; set; }
    }

}
