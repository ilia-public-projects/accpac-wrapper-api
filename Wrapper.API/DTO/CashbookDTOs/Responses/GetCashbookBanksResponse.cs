using AutoMapper;
using Wrapper.API.DTO.CashbookDTOs.Models;
using Wrapper.Models.Accpac.CashbookModels.CashbookSetupModels;

namespace Wrapper.API.DTO.CashbookDTOs.Responses
{
    /// <summary>
    /// Represents the response containing a list of cashbook banks.
    /// </summary>
    public class GetCashbookBanksResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCashbookBanksResponse"/> class.
        /// Maps the source list of <see cref="BankModel"/> objects to a list of <see cref="ResponseBankModel"/> objects using the provided mapper.
        /// </summary>
        /// <param name="mapper">The mapper used to map the source list of bank models to the response models.</param>
        /// <param name="source">The source list of bank models to be mapped.</param>
        public GetCashbookBanksResponse(
                IMapper mapper, List<BankModel> source
            )
        {
            Banks = mapper.Map<List<ResponseBankModel>>(source);
        }

        /// <summary>
        /// Gets or sets the list of cashbook banks, represented by <see cref="ResponseBankModel"/> objects.
        /// </summary>
        public List<ResponseBankModel> Banks { get; set; }
    }

}
