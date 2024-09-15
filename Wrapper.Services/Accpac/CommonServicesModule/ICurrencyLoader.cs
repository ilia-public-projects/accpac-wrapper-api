using Wrapper.Models.Accpac.CommonServicesModels;

namespace Wrapper.Services.Accpac.CommonServicesModule
{
    /// <summary>
    /// Provides functionality for loading currency data from accpac.
    /// </summary>
    public interface ICurrencyLoader
    {
        /// <summary>
        /// Retrieves a list of currencies from the database asynchronously.
        /// </summary>
        /// <param name="context">The operation context containing information about the current operation.</param>
        /// <returns>A list of <see cref="CurrencyModel"/> objects representing available currencies.</returns>
        Task<List<CurrencyModel>> GetCurrenciesAsync(IOperationContext context);
    }
}
