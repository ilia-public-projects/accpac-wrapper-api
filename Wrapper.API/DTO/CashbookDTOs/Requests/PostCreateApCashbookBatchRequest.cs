using Wrapper.API.DTO.CashbookDTOs.Models;

namespace Wrapper.API.DTO.CashbookDTOs.Requests
{
    /// <summary>
    /// Represents the request to create an Accounts Payable cashbook batch, 
    /// inheriting common properties from <see cref="PostCreateCashbookBatchRequestBase"/>.
    /// </summary>
    public class PostCreateApCashbookBatchRequest : PostCreateCashbookBatchRequestBase
    {
        /// <summary>
        /// Gets or sets the list of cashbook batch header entries for the request.
        /// </summary>
        public List<RequestApCashbookBatchHeader> Headers { get; set; }
    }

}
