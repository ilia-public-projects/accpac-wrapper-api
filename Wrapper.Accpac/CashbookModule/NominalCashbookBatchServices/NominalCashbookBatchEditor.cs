using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels.NominalCashbookBatchModels;
using Wrapper.Services;
using Wrapper.Services.Accpac.CashbookModule.NominalCashbookBatchServices;

namespace Wrapper.Accpac.CashbookModule.NominalCashbookBatchServices
{
    public class NominalCashbookBatchEditor : INominalCashbookBatchEditor
    {
        private readonly NominalCashbookBatchInsertProcessor insertProcessor;

        public NominalCashbookBatchEditor(
                NominalCashbookBatchInsertProcessor insertProcessor
            )
        {

            this.insertProcessor = insertProcessor;
        }

        public async Task CreateNominalCashbookBatchAsync(IOperationContext context, NominalCashbookBatch model)
        {
            await insertProcessor.InsertAsync(context, model);
        }
    }
}
