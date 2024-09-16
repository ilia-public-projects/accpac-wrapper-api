using AutoMapper;
using Wrapper.API.DTO.APDTOs.Models;
using Wrapper.API.DTO.APDTOs.Requests;
using Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels;

namespace Wrapper.API.Mapping
{
    public class APInvoiceBatchProfile:Profile
    {
        public APInvoiceBatchProfile()
        {
            CreateMap<ApInvoiceBatchIdentifier, ResponseApInvoiceBatchIdentifier>();
            CreateMap<RequestApInvoiceBatchDetailEntryModel, ApInvoiceBatchDetail>();
            CreateMap<RequestApInvoiceBatchHeaderEntryModel, ApInvoiceBatchHeader>();
            CreateMap<PostCreateAPInvoiceBatchRequest, ApInvoiceBatch>();
        }
    }
}
