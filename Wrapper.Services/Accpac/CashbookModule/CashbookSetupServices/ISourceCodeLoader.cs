using Wrapper.Models.Accpac.CashbookModels.CashbookSetupModels;

namespace Wrapper.Services.Accpac.CashbookModule.CashbookSetupServices
{
    /// <summary>
    /// Loads source code information from the accpac.
    /// </summary>
    public interface ISourceCodeLoader
    {
        /// <summary>
        /// Retrieves a list of source codes from the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="SourceCodeModel"/> objects representing the source codes.</returns>
        Task<List<SourceCodeModel>> GetSourceCodesAsync();
    }
}
