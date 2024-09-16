using AccpacCOMAPI;
using Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels;
using Wrapper.Services;
using Wrapper.Services.Accpac.APModule.APInvoiceBatchServices;

namespace Wrapper.Accpac.APModule.APInvoiceBatchServices
{
    public class ApInvoiceBatchViewProcessor: IApInvoiceBatchViewProcessor
    {
        public void CreateBatch(IOperationContext context, ApInvoiceBatchView view, string batchName, DateTime batchDate)
        {
            view.BatchView.RecordCreate(tagViewRecordCreateEnum.VIEW_RECORD_CREATE_INSERT);
            view.BatchView.Fields.FieldByName["BTCHDESC"].set_Value(batchName);
            view.BatchView.Fields.FieldByName["DATEBTCH"].set_Value(batchDate);
            view.BatchView.Update();
        }

        public void CreateHeader(IOperationContext context, ApInvoiceBatchView view, ApInvoiceBatchHeader header)
        {
            // create header
            // process commands required to make sure, tax calculation error is not shown
            view.HeaderView.RecordCreate(tagViewRecordCreateEnum.VIEW_RECORD_CREATE_NOINSERT);
            view.HeaderView.Fields.FieldByName["DATEINVC"].set_Value(header.InvoiceDate);
            view.HeaderView.Fields.FieldByName["TEXTTRX"].set_Value(header.TransactionType); // AP
            view.HeaderView.Fields.FieldByName["PROCESSCMD"].set_Value("7");
            view.HeaderView.Process();
            view.HeaderView.Fields.FieldByName["PROCESSCMD"].set_Value("4");
            view.HeaderView.Process();
            view.HeaderView.Fields.FieldByName["IDVEND"].set_Value(header.VendorId);
            view.HeaderView.Fields.FieldByName["PROCESSCMD"].set_Value("7");
            view.HeaderView.Process();
            view.HeaderView.Fields.FieldByName["IDINVC"].set_Value(header.InvoiceNo);
            view.HeaderView.Fields.FieldByName["INVCDESC"].set_Value(header.Description);

            // set gross total
            view.HeaderView.Fields.FieldByName["AMTGROSTOT"].set_Value(header.TotalAmount);

            view.HeaderView.Fields.FieldByName["TAXCLASS1"].set_Value("3"); // no tax

            view.HeaderView.Process();
        }

        public void CreateHeaderOptionalField(IOperationContext context, ApInvoiceBatchView view, ApInvoiceBatchHeader header)
        {
            // create header optional field
            view.HeaderOptFieldView.RecordCreate(tagViewRecordCreateEnum.VIEW_RECORD_CREATE_NOINSERT);
            view.HeaderOptFieldView.Fields.FieldByName["OPTFIELD"].set_Value("CREDITCODE");
            view.HeaderOptFieldView.Fields.FieldByName["SWSET"].set_Value("1");
            view.HeaderOptFieldView.Fields.FieldByName["VALIFTEXT"].set_Value(header.CreditCode);
            view.HeaderOptFieldView.Insert();
        }

        public void AddLinesToHeader(IOperationContext context, ApInvoiceBatchView view, ApInvoiceBatchHeader header)
        {
            foreach (ApInvoiceBatchDetail detail in header.Details)
            {
                view.DetailView.RecordCreate(tagViewRecordCreateEnum.VIEW_RECORD_CREATE_NOINSERT);
                view.DetailView.Fields.FieldByName["TEXTDESC"].set_Value(detail.Description);
                view.DetailView.Fields.FieldByName["IDGLACCT"].set_Value(detail.AccountId.Trim());

                view.DetailView.Fields.FieldByName["AMTDIST"].set_Value(detail.Amount);
                view.DetailView.Fields.FieldByName["AMTGLDIST"].set_Value(detail.Amount);
                view.DetailView.Fields.FieldByName["AMTDISTHC"].set_Value(detail.Amount);
                view.DetailView.Fields.FieldByName["DISTNETHC"].set_Value(detail.Amount);

                view.DetailView.Fields.FieldByName["RATETAX1"].set_Value(0);
                view.DetailView.Fields.FieldByName["RATETAX2"].set_Value(0);
                view.DetailView.Fields.FieldByName["AMTTOTTAX"].set_Value(0);

                view.DetailView.Insert();

                view.DetailView.RecordClear();
            }
        }

        public void InsertHeader(IOperationContext context, ApInvoiceBatchView view)
        {
            view.HeaderView.Insert();
            view.HeaderView.Update();
            view.HeaderView.RecordClear();
        }

        public void UpdateBatch(IOperationContext context, ApInvoiceBatchView view)
        {
            view.BatchView.Update();
        }
    }
}
