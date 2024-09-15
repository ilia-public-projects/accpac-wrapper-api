using Wrapper.Models.Accpac.CashbookModels.CashbookSetupModels;

namespace Wrapper.Services.Accpac.CashbookModule.CashbookSetupServices
{
    /// <summary>
    /// Loads bank information from the accpac
    /// </summary>
    public interface IBankLoader
    {
        /// <summary>
        /// Retrieves a list of active banks from the database.
        /// </summary>
        /// <param name="context">The operation context containing details about the current operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="BankModel"/> objects representing the active banks.</returns>
        Task<List<BankModel>> GetActiveBanksAsync(IOperationContext context);
    }
}
