using Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels;

namespace Wrapper.Services.Accpac.CashbookModule
{
    /// <summary>
    /// Composes and builds the cashbook batch view using various Accpac views.
    /// </summary>
    public interface ICashbookBatchViewComposer
    {
        /// <summary>
        /// Builds the cashbook batch view by composing multiple Accpac views into a cohesive structure.
        /// </summary>
        /// <param name="context">The operation context containing information about the current operation.</param>
        /// <returns>A <see cref="CashbookBatchView"/> object containing the composed views for the cashbook batch.</returns>
        CashbookBatchView BuildBatchView(IOperationContext context);
    }
}
