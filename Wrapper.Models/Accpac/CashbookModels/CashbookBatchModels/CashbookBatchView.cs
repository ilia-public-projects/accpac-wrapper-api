using AccpacCOMAPI;

namespace Wrapper.Models.Accpac.CashbookModels.CashbookBatchModels
{
    /// <summary>
    /// Represents a collection of Accpac views related to cashbook batch processing,
    /// including batch, header, detail, and optional fields views.
    /// </summary>
    public class CashbookBatchView
    {
        /// <summary>
        /// Gets or sets the control view for the cashbook batch.
        /// Corresponding Accpac view number: "CB0009".
        /// </summary>
        public AccpacView BatchView { get; set; }

        /// <summary>
        /// Gets or sets the header view for the cashbook batch.
        /// Corresponding Accpac view number: "CB0010".
        /// </summary>
        public AccpacView HeaderView { get; set; }

        /// <summary>
        /// Gets or sets the detail view for the cashbook batch.
        /// Corresponding Accpac view number: "CB0011".
        /// </summary>
        public AccpacView DetailView { get; set; }

        /// <summary>
        /// Gets or sets the sub-detail view for the cashbook batch.
        /// Corresponding Accpac view number: "CB0012".
        /// </summary>
        public AccpacView SubDetailView { get; set; }

        /// <summary>
        /// Gets or sets the miscellaneous view for the cashbook batch.
        /// Corresponding Accpac view number: "CB0014".
        /// </summary>
        public AccpacView MiscView { get; set; }

        /// <summary>
        /// Gets or sets the header optional fields view for the cashbook batch.
        /// Corresponding Accpac view number: "CB0404".
        /// </summary>
        public AccpacView HeaderOptFieldsView { get; set; }
    }

}
