using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels.ApCashbookBatchModels;

namespace Wrapper.Services.Accpac.CashbookModule.ApCashbookBatchServices
{
    /// <summary>
    /// Validates the entries in an Accounts Payable (AP) cashbook batch.
    /// </summary>
    public interface IApCashbookBatchValidator
    {
        /// <summary>
        /// Validates the AP cashbook batch entries.
        /// </summary>
        /// <param name="context">The operation context.</param>
        /// <param name="model">The AP cashbook batch entry model to validate.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task ValidateBatchAsync(IOperationContext context, ApCashbookBatch model);
    }
}
