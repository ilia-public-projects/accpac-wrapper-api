using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wrapper.API.DTO;
using Wrapper.API.DTO.CashbookDTOs.Responses;
using Wrapper.API.Extensions;
using Wrapper.Models.Accpac.CashbookModels.CashbookSetupModels;
using Wrapper.RequestFilters;
using Wrapper.Services;
using Wrapper.Services.Accpac.CashbookModule.CashbookSetupServices;

namespace Wrapper.API.Controllers
{
    [Route("api/v1/cashbook/setup")]
    [ApiController]
    public class CashbookSetupController : BaseController
    {
        private readonly IBankLoader bankLoader;
        private readonly IMapper mapper;

        public CashbookSetupController(
                IBankLoader bankLoader,
                IMapper mapper
            )
        {
            this.bankLoader = bankLoader;
            this.mapper = mapper;
        }

        /// <summary>
        /// Retrieves all active banks from the Sage 300 Cashbook (CB) module.
        /// </summary>
        /// <returns>A response containing the list of active banks.</returns>
        [HttpGet]
        [Authorize]
        [Route("banks/active")]
        public async Task<APIResponse<GetCashbookBanksResponse>> GetActiveBanks()
        {
            IOperationContext context = HttpContext.GetOperationContext();
            List<BankModel> banks = await bankLoader.GetActiveBanksAsync(context);
            return new APIResponse<GetCashbookBanksResponse>(new GetCashbookBanksResponse(mapper, banks));
        }

    }
}
