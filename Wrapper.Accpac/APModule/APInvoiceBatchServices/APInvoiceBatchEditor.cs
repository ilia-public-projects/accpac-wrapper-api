using Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels;
using Wrapper.Services;
using Wrapper.Services.Accpac.APModule.APInvoiceBatchServices;

namespace Wrapper.Accpac.APModule.APInvoiceBatchServices
{
    public class APInvoiceBatchEditor : IAPInvoiceBatchEditor
    {
        private readonly ApInvoiceBatchInsertProcessor apInvoiceBatchInsertProcessor;

        public APInvoiceBatchEditor(
                ApInvoiceBatchInsertProcessor apInvoiceBatchInsertProcessor
            )
        {
            this.apInvoiceBatchInsertProcessor = apInvoiceBatchInsertProcessor;
        }


        public async Task CreateBatchAsync(IOperationContext context, ApInvoiceBatch model)
        {
            await apInvoiceBatchInsertProcessor.InsertAsync(context, model);
        }
    }
}
