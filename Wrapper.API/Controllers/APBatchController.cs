using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wrapper.API.DTO;
using Wrapper.API.DTO.APDTOs.Requests;
using Wrapper.API.DTO.APDTOs.Responses;
using Wrapper.API.Extensions;
using Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels;
using Wrapper.RequestFilters;
using Wrapper.Services;
using Wrapper.Services.Accpac.APModule.APInvoiceBatchServices;

namespace Wrapper.API.Controllers
{
    [Route("api/v1/accounts-payable/batch")]
    [ApiController]
    public class APBatchController : BaseController
    {
        private readonly IMapper mapper;
        private readonly IAPInvoiceBatchEditor apInvoiceBatchEditor;
        private readonly IApInvoiceBatchLoader apInvoiceBatchLoader;

        public APBatchController(
                IMapper mapper,
                IAPInvoiceBatchEditor apInvoiceBatchEditor,
                IApInvoiceBatchLoader apInvoiceBatchLoader
            )
        {
            this.mapper = mapper;
            this.apInvoiceBatchEditor = apInvoiceBatchEditor;
            this.apInvoiceBatchLoader = apInvoiceBatchLoader;
        }

        /// <summary>
        /// Creates an Accounts Payable invoice batch in Sage 300.
        /// </summary>
        /// <remarks>
        /// This endpoint allows clients to create a new AP invoice batch with headers and lines as specified in the request.
        /// </remarks>
        /// <param name="request">The request containing batch headers and lines for the new batch.</param>
        /// <returns>A response containing the result of the batch creation operation.</returns>
        [HttpPost]
        [Authorize]
        [Route("invoice")]
        public async Task<APIResponse<PostCreateAPInvoiceBatchResponse>> CreateInvoiceBatch([FromBody] PostCreateAPInvoiceBatchRequest request)
        {
            IOperationContext context = HttpContext.GetOperationContext();
            ApInvoiceBatch model = mapper.Map<ApInvoiceBatch>(request);
            await apInvoiceBatchEditor.CreateBatchAsync(context, model);
            return new APIResponse<PostCreateAPInvoiceBatchResponse>(new PostCreateAPInvoiceBatchResponse());
        }

        /// <summary>
        /// Retrieves a list of all active Accounts Payable invoice batch identifiers.
        /// </summary>
        /// <returns>A response containing the list of active AP invoice batch identifiers.</returns>
        [HttpGet]
        [Authorize]
        [Route("identifiers/active")]
        public async Task<APIResponse<GetApInvoiceBatchIdentifiersResponse>> GetActiveBatchIdentifiers()
        {
            IOperationContext context = HttpContext.GetOperationContext();
            List<ApInvoiceBatchIdentifier> batches = await apInvoiceBatchLoader.GetActiveBatchIdentifiersAsync(context);
            return new APIResponse<GetApInvoiceBatchIdentifiersResponse>(new GetApInvoiceBatchIdentifiersResponse(mapper, batches));
        }
    }
}
