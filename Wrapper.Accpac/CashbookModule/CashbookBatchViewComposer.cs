using AccpacCOMAPI;
using Wrapper.Models.Accpac;
using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels;
using Wrapper.Services;
using Wrapper.Services.Accpac.CashbookModule;

namespace Wrapper.Accpac.CashbookModule
{
    public class CashbookBatchViewComposer : ICashbookBatchViewComposer
    {
        public CashbookBatchView BuildBatchView(IOperationContext context)
        {
            // open views
            context.AccpacDBLink.OpenView(AccpacViewConstants.CBBatchControlView, out AccpacView batchCtrlView);
            context.AccpacDBLink.OpenView(AccpacViewConstants.CBBatchHeaderView, out AccpacView batchHeaderView);
            context.AccpacDBLink.OpenView(AccpacViewConstants.CBBatchDetailView, out AccpacView batchDetailView);
            context.AccpacDBLink.OpenView(AccpacViewConstants.CBBatchSubDetailView, out AccpacView batchSubDetailView);
            context.AccpacDBLink.OpenView(AccpacViewConstants.CBBatchMiscView, out AccpacView batchMiscView);
            context.AccpacDBLink.OpenView(AccpacViewConstants.CBBatchHeaderOptionalFieldsView, out AccpacView batchHeaderOptionalFieldsView);

            // compose views
            batchCtrlView.Compose(batchHeaderView);
            batchHeaderView.Compose(new AccpacView[] { 
                batchCtrlView, 
                batchDetailView,
                batchMiscView,
                batchHeaderOptionalFieldsView });
            batchDetailView.Compose(new AccpacView[] {
                batchHeaderView, 
                batchSubDetailView
            });


            batchSubDetailView.Compose(new AccpacView[] { 
                batchDetailView
            });

            batchHeaderOptionalFieldsView.Compose(new AccpacView[] { batchHeaderView });

            var result = new CashbookBatchView
            {
                BatchView = batchCtrlView,
                HeaderView = batchHeaderView,
                DetailView = batchDetailView,
                SubDetailView = batchSubDetailView,
                HeaderOptFieldsView = batchHeaderOptionalFieldsView
            };

            return result;
        }
    }
}
