using AutoMapper;
using Wrapper.API.DTO.APDTOs.Models;
using Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels;

namespace Wrapper.API.DTO.APDTOs.Responses
{
    /// <summary>
    /// Represents the response containing a list of Accounts Payable invoice batch identifiers.
    /// </summary>
    public class GetApInvoiceBatchIdentifiersResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetApInvoiceBatchIdentifiersResponse"/> class.
        /// Maps the source list of <see cref="ApInvoiceBatchIdentifier"/> to the response list of 
        /// <see cref="ResponseApInvoiceBatchIdentifier"/> using the provided mapper.
        /// </summary>
        /// <param name="mapper">The mapper used to map the source list to the response list.</param>
        /// <param name="source">The source list of invoice batch identifiers to be mapped.</param>
        public GetApInvoiceBatchIdentifiersResponse(
                IMapper mapper, List<ApInvoiceBatchIdentifier> source
            )
        {
            Batches = mapper.Map<List<ResponseApInvoiceBatchIdentifier>>(source);
        }

        /// <summary>
        /// Gets or sets the list of mapped Accounts Payable invoice batch identifiers.
        /// </summary>
        public List<ResponseApInvoiceBatchIdentifier> Batches { get; set; }
    }

}
