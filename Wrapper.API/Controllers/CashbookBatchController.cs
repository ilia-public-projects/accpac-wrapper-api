using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wrapper.API.DTO;
using Wrapper.API.DTO.CashbookDTOs.Requests;
using Wrapper.API.DTO.CashbookDTOs.Responses;
using Wrapper.API.Extensions;
using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels;
using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels.ApCashbookBatchModels;
using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels.NominalCashbookBatchModels;
using Wrapper.RequestFilters;
using Wrapper.Services;
using Wrapper.Services.Accpac.CashbookModule;
using Wrapper.Services.Accpac.CashbookModule.ApCashbookBatchServices;
using Wrapper.Services.Accpac.CashbookModule.NominalCashbookBatchServices;

namespace Wrapper.API.Controllers
{
    [Route("api/v1/cashbook/batch")]
    [ApiController]
    public class CashbookBatchController : BaseController
    {
        private readonly IMapper mapper;
        private readonly INominalCashbookBatchEditor cashbookBatchEditor;
        private readonly IApCashbookBatchEditor apCashbookBatchEditor;
        private readonly ICashbookBatchLoader cashbookBatchLoader;

        public CashbookBatchController(
                IMapper mapper,
                INominalCashbookBatchEditor cashbookBatchEditor,
                IApCashbookBatchEditor apCashbookBatchEditor,
                ICashbookBatchLoader cashbookBatchLoader
            )
        {
            this.mapper = mapper;
            this.cashbookBatchEditor = cashbookBatchEditor;
            this.apCashbookBatchEditor = apCashbookBatchEditor;
            this.cashbookBatchLoader = cashbookBatchLoader;
        }

        /// <summary>
        /// Creates a nominal cashbook batch in Sage 300.
        /// </summary>
        /// <param name="request">Contains the details of the new batch, including headers and lines.</param>
        /// <returns>A response containing the result of the nominal cashbook batch creation.</returns>
        [HttpPost]
        [Authorize]
        [Route("nominal")]
        public async Task<APIResponse<PostCreateNominalCashbookBatchResponse>> CreateNominalBatch([FromBody] PostCreateNominalCashbookBatchRequest request)
        {
            IOperationContext context = HttpContext.GetOperationContext();
            NominalCashbookBatch model = mapper.Map<NominalCashbookBatch>(request);
            await cashbookBatchEditor.CreateNominalCashbookBatchAsync(context, model);
            return new APIResponse<PostCreateNominalCashbookBatchResponse>(new PostCreateNominalCashbookBatchResponse());
        }

        /// <summary>
        /// Creates an Accounts Payable (AP) cashbook batch in Sage 300.
        /// </summary>
        /// <param name="request">Contains the details of the new AP batch, including headers and lines.</param>
        /// <returns>A response containing the result of the AP cashbook batch creation.</returns>
        [HttpPost]
        [Authorize]
        [Route("ap")]
        public async Task<APIResponse<PostCreateApCashbookBatchResponse>> CreateApBatch([FromBody] PostCreateApCashbookBatchRequest request)
        {
            IOperationContext context = HttpContext.GetOperationContext();
            ApCashbookBatch model = mapper.Map<ApCashbookBatch>(request);
            await apCashbookBatchEditor.CreateBatchAsync(context, model);
            return new APIResponse<PostCreateApCashbookBatchResponse>(new PostCreateApCashbookBatchResponse());
        }

        /// <summary>
        /// Retrieves all active cashbook batch identifiers.
        /// </summary>
        /// <returns>A response containing the list of active cashbook batch identifiers.</returns>
        [HttpGet]
        [Authorize]
        [Route("identifiers/active")]
        public async Task<APIResponse<GetCashbookBatchIdentifiersResponse>> GetActiveBatchIdentifiers()
        {
            IOperationContext context = HttpContext.GetOperationContext();
            List<CashbookBatchIdentifier> identifiers = await cashbookBatchLoader.GetActiveBatchIdentifiersAsync(context);
            return new APIResponse<GetCashbookBatchIdentifiersResponse>(new GetCashbookBatchIdentifiersResponse(mapper, identifiers));
        }

    }
}
