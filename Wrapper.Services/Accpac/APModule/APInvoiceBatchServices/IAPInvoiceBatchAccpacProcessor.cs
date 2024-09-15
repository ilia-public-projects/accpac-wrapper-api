using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels;

namespace Wrapper.Services.Accpac.APModule.APInvoiceBatchServices
{
    public interface IAPInvoiceBatchAccpacProcessor
    {
        void AddLinesToHeader(IOperationContext context, ApInvoiceBatchView view, ApInvoiceBatchHeaderEntryModel header);
        void CreateBatch(IOperationContext context, ApInvoiceBatchView view, string batchName, DateTime batchDate);
        void CreateHeader(IOperationContext context, ApInvoiceBatchView view, ApInvoiceBatchHeaderEntryModel header);
        void CreateHeaderOptionalField(IOperationContext context, ApInvoiceBatchView view, ApInvoiceBatchHeaderEntryModel header);
        void InsertHeader(IOperationContext context, ApInvoiceBatchView view);
        void UpdateBatch(IOperationContext context, ApInvoiceBatchView view);
    }
}
