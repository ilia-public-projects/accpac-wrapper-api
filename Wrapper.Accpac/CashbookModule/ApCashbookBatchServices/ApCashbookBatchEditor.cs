using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels.ApCashbookBatchModels;
using Wrapper.Services;
using Wrapper.Services.Accpac.CashbookModule.ApCashbookBatchServices;

namespace Wrapper.Accpac.CashbookModule.ApCashbookBatchServices
{
    public class ApCashbookBatchEditor : IApCashbookBatchEditor
    {
        private readonly ApCashbookBatchInsertProcessor insertProcessor;

        public ApCashbookBatchEditor(
                ApCashbookBatchInsertProcessor insertProcessor
            )
        {
            this.insertProcessor = insertProcessor;
        }

        public async Task CreateBatchAsync(IOperationContext context, ApCashbookBatch model)
        {
            await insertProcessor.InsertAsync(context, model);
        }
    }
}
