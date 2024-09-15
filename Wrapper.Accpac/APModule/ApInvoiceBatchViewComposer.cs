using AccpacCOMAPI;
using Wrapper.Models.Accpac;
using Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels;
using Wrapper.Services;
using Wrapper.Services.Accpac.APModule;

namespace Wrapper.Accpac.APModule
{
    public class ApInvoiceBatchViewComposer: IApInvoiceBatchViewComposer
    {
        public ApInvoiceBatchView BuildBatchView(IOperationContext context)
        {
            // open views
            context.AccpacDBLink.OpenView(AccpacViewConstants.APBatchControlView, out AccpacView batchCtrlView);
            context.AccpacDBLink.OpenView(AccpacViewConstants.APBatchHeaderView, out AccpacView batchHeaderView);
            context.AccpacDBLink.OpenView(AccpacViewConstants.APBatchDetailView, out AccpacView batchDetailView);
            context.AccpacDBLink.OpenView(AccpacViewConstants.APPaymentScheduleView, out AccpacView paymentScheduleView);
            context.AccpacDBLink.OpenView(AccpacViewConstants.APBatchDetailOptionalFieldsView, out AccpacView batchDetailOptionalFieldsView);
            context.AccpacDBLink.OpenView(AccpacViewConstants.APBatchHeaderOptionalFieldsView, out AccpacView batchHeaderOptionalFieldsView);

            // compose views
            batchCtrlView.Compose(batchHeaderView);
            batchHeaderView.Compose(new AccpacView[] { batchCtrlView, batchDetailView, paymentScheduleView, batchHeaderOptionalFieldsView });
            batchDetailView.Compose(new AccpacView[] { batchHeaderView, batchCtrlView, batchDetailOptionalFieldsView });

            paymentScheduleView.Compose(new AccpacView[] { batchHeaderView });
            batchDetailOptionalFieldsView.Compose(new AccpacView[] { batchDetailView });
            batchHeaderOptionalFieldsView.Compose(new AccpacView[] { batchHeaderView });

            var result = new ApInvoiceBatchView
            {
                BatchView = batchCtrlView,
                HeaderView = batchHeaderView,
                DetailView = batchDetailView,
                PaymentScheduleView = paymentScheduleView,
                DetailOptFieldView = batchDetailOptionalFieldsView,
                HeaderOptFieldView = batchHeaderOptionalFieldsView
            };

            return result;
        }
    }
}
