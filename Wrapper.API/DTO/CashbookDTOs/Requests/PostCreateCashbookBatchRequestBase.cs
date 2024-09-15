using System.ComponentModel.DataAnnotations;

namespace Wrapper.API.DTO.CashbookDTOs.Requests
{
    /// <summary>
    /// Represents the base class for creating a cashbook batch request.
    /// Contains common properties such as batch name and bank code.
    /// </summary>
    public class PostCreateCashbookBatchRequestBase
    {
        /// <summary>
        /// Gets or sets the name of the batch.
        /// </summary>
        /// <example>CashbookBatch2024Q3</example>
        [Required]
        public string BatchName { get; set; }

        /// <summary>
        /// Gets or sets the code of the bank associated with the batch.
        /// </summary>
        /// <example>BANK123</example>
        [Required]
        public string BankCode { get; set; }
    }
}
