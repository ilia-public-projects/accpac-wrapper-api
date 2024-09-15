using AutoMapper;
using Wrapper.API.DTO.CashbookDTOs.Models;
using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels;

namespace Wrapper.API.DTO.CashbookDTOs.Responses
{
    /// <summary>
    /// Represents the response containing a list of cashbook batch identifiers.
    /// </summary>
    public class GetCashbookBatchIdentifiersResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCashbookBatchIdentifiersResponse"/> class.
        /// Maps the source list of <see cref="CashbookBatchIdentifier"/> objects to a list of <see cref="ResponseCashbookBatchIdentifier"/> objects using the provided mapper.
        /// </summary>
        /// <param name="mapper">The mapper used to map the source list of batch identifiers to the response models.</param>
        /// <param name="source">The source list of cashbook batch identifiers to be mapped.</param>
        public GetCashbookBatchIdentifiersResponse(
                IMapper mapper, List<CashbookBatchIdentifier> source
            )
        {
            Batches = mapper.Map<List<ResponseCashbookBatchIdentifier>>(source);
        }

        /// <summary>
        /// Gets or sets the list of cashbook batch identifiers, represented by <see cref="ResponseCashbookBatchIdentifier"/> objects.
        /// </summary>
        public List<ResponseCashbookBatchIdentifier> Batches { get; set; }
    }

}
