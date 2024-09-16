using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels.ApCashbookBatchModels;

namespace Wrapper.Services.Accpac.CashbookModule.ApCashbookBatchServices
{
    public interface IApCashbookBatchEditor
    {
        Task CreateBatchAsync(IOperationContext context, ApCashbookBatch model);
    }
}
