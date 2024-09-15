using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wrapper.API.DTO;
using Wrapper.API.DTO.GLDTOs.Responses;
using Wrapper.API.Extensions;
using Wrapper.Models.Accpac.GLModels.GLSetupModels;
using Wrapper.RequestFilters;
using Wrapper.Services;
using Wrapper.Services.Accpac.GLModule.GLSetupServices;

namespace Wrapper.API.Controllers
{
    [Route("api/v1/gl/setup")]
    [ApiController]
    public class GLSetupController : BaseController
    {
        private readonly IGLAccountLoader gLAccountLoader;
        private readonly IMapper mapper;

        public GLSetupController(
                IGLAccountLoader gLAccountLoader,
                IMapper mapper
            )
        {
            this.gLAccountLoader = gLAccountLoader;
            this.mapper = mapper;
        }

        /// <summary>
        /// Retrieves all active General Ledger (GL) accounts from the Sage 300 GL module.
        /// </summary>
        /// <returns>A response containing the list of active GL accounts.</returns>
        [HttpGet]
        [Authorize]
        [Route("accounts/active")]
        public async Task<APIResponse<GetGLAccountsResponse>> GetActiveAccounts()
        {
            IOperationContext context = HttpContext.GetOperationContext();
            List<GLAccountModel> accounts = await gLAccountLoader.GetActiveAccountsAsync(context);
            return new APIResponse<GetGLAccountsResponse>(new GetGLAccountsResponse(mapper, accounts));
        }

    }
}
