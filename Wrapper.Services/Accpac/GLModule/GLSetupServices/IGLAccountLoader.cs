using Wrapper.Models.Accpac.GLModels.GLSetupModels;

namespace Wrapper.Services.Accpac.GLModule.GLSetupServices
{
    /// <summary>
    /// Provides functionality to load active General Ledger (GL) accounts from the accpac
    /// </summary>
    public interface IGLAccountLoader
    {
        /// <summary>
        /// Retrieves a list of active GL accounts from the database asynchronously.
        /// </summary>
        /// <param name="context">The operation context containing information about the current operation.</param>
        /// <returns>A list of active <see cref="GLAccountModel"/> objects representing GL accounts.</returns>
        Task<List<GLAccountModel>> GetActiveAccountsAsync(IOperationContext context);
    }
}
