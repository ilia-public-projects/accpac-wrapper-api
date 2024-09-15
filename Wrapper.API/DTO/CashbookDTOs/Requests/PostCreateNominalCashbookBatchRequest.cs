using System.ComponentModel.DataAnnotations;
using Wrapper.API.DTO.CashbookDTOs.Models;
using Wrapper.Models.Accpac.CashbookModels;

namespace Wrapper.API.DTO.CashbookDTOs.Requests
{
    public class PostCreateNominalCashbookBatchRequest : PostCreateCashbookBatchRequestBase
    {

        [Required]
        public List<RequestCashbookBatchHeaderBase> Headers { get; set; }
    }
}
