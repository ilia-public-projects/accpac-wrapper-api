using AccpacCOMAPI;

namespace Wrapper.Services.Accpac
{
    /// <summary>
    /// Interface that provides a wrapper around the AccpacDBLink object, 
    /// exposing methods to manage transactions and interactions with Accpac views.
    /// </summary>
    public interface IAccpacDbLinkWrapper
    {
        /// <summary>
        /// Begins a new transaction within the Accpac system.
        /// </summary>
        /// <param name="p">An output parameter representing the transaction state.</param>
        void TransactionBegin(out int p);

        /// <summary>
        /// Commits the current transaction in the Accpac system.
        /// </summary>
        /// <param name="p">An output parameter representing the transaction state.</param>
        void TransactionCommit(out int p);

        /// <summary>
        /// Rolls back the current transaction in the Accpac system.
        /// </summary>
        /// <param name="p">An output parameter representing the transaction state.</param>
        void TransactionRollback(out int p);

        /// <summary>
        /// Opens an Accpac view using the specified view ID.
        /// </summary>
        /// <param name="viewId">The ID of the Accpac view to open.</param>
        /// <param name="accpacView">An output parameter representing the opened Accpac view.</param>
        void OpenView(string viewId, out AccpacView accpacView);

        /// <summary>
        /// Retrieves the fiscal calendar from the Accpac system.
        /// </summary>
        /// <returns>An instance of <see cref="AccpacFiscalCalendar"/> representing the fiscal calendar.</returns>
        AccpacFiscalCalendar GetFiscalCalendar();

        /// <summary>
        /// Closes the connection to the Accpac system.
        /// </summary>
        void Close();
    }

}
