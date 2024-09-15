using System.ComponentModel.DataAnnotations;
using Wrapper.API.DTO.APDTOs.Models;

namespace Wrapper.API.DTO.APDTOs.Requests
{
    /// <summary>
    /// Represents a request to create an Accounts Payable invoice batch.
    /// </summary>
    public class PostCreateAPInvoiceBatchRequest
    {
        /// <summary>
        /// Name of the batch.
        /// </summary>
        /// <example>Batch20230914</example>
        [Required]
        public string BatchName { get; set; }

        /// <summary>
        /// The date when the batch is created.
        /// </summary>
        /// <example>2024-05-14</example>
        [Required]
        public DateTime BatchDate { get; set; }

        /// <summary>
        /// The list of invoice batch header entries.
        /// </summary>
        [Required]
        public List<RequestApInvoiceBatchHeaderEntryModel> Headers { get; set; }
    }
}
