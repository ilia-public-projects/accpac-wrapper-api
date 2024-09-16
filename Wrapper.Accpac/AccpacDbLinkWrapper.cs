using AccpacCOMAPI;
using Wrapper.Services.Accpac;

namespace Wrapper.Accpac
{
    public class AccpacDbLinkWrapper : IAccpacDbLinkWrapper
    {
        private readonly AccpacDBLink accpacDBLink;

        public AccpacDbLinkWrapper(AccpacDBLink accpacDBLink)
        {
            this.accpacDBLink = accpacDBLink;
        }

        public void Close()
        {
            accpacDBLink.Close();
        }

        public AccpacFiscalCalendar GetFiscalCalendar()
        {
            return accpacDBLink.GetFiscalCalendar();
        }

        public void OpenView(string viewId, out AccpacView accpacView)
        {
            accpacDBLink.OpenView(viewId, out accpacView);
        }

        public void TransactionBegin(out int p)
        {
            accpacDBLink.TransactionBegin(out p);
        }

        public void TransactionCommit(out int p)
        {
            accpacDBLink.TransactionCommit(out p);
        }

        public void TransactionRollback(out int p)
        {
            accpacDBLink.TransactionRollback(out p);
        }
    }
}
