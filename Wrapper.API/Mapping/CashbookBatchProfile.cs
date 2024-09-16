using AutoMapper;
using Wrapper.API.DTO.CashbookDTOs.Models;
using Wrapper.API.DTO.CashbookDTOs.Requests;
using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels;
using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels.ApCashbookBatchModels;
using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels.NominalCashbookBatchModels;
using Wrapper.Models.Accpac.CashbookModels.CashbookSetupModels;

namespace Wrapper.API.Mapping
{
    public class CashbookBatchProfile:Profile
    {
        public CashbookBatchProfile()
        {
            CreateMap<PostCreateNominalCashbookBatchRequest, NominalCashbookBatch>();
            CreateMap<PostCreateCashbookBatchRequestBase,CashbookBatchBase>();
            CreateMap<PostCreateApCashbookBatchRequest, ApCashbookBatch>();

            CreateMap<RequestApCashbookBatchHeader, ApCashbookBatchHeader>();
            CreateMap<RequestCashbookBatchHeaderBase, CashbookBatchHeader>();
            CreateMap<RequestCashbookBatchDetail, CashbookBatchDetail>();

            CreateMap<BankModel,ResponseBankModel>();
            CreateMap<CashbookBatchIdentifier, ResponseCashbookBatchIdentifier>();
        }
    }
}
