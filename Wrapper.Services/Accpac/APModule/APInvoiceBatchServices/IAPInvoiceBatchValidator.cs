using Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels;

namespace Wrapper.Services.Accpac.APModule.APInvoiceBatchServices
{
    /// <summary>
    /// Validates AP invoice batch entries, ensuring compliance with accounting rules and optional field requirements.
    /// </summary>
    public interface IAPInvoiceBatchValidator
    {
        /// <summary>
        /// Validates an AP invoice batch entry model to ensure all required fields are correct and complete.
        /// </summary>
        /// <param name="context">The operation context providing access to the necessary resources.</param>
        /// <param name="model">The AP invoice batch entry model to validate.</param>
        /// <returns>A task representing the asynchronous validation operation.</returns>
        Task ValidateCreateInvoiceBatchAsync(IOperationContext context, ApInvoiceBatchEntryModel model);
    }
}
