using AccpacCOMAPI;

namespace Wrapper.Models.Accpac.APModels.ApInvoiceBatchModels
{
    /// <summary>
    /// Represents the collection of Accpac views related to an Accounts Payable (AP) invoice batch,
    /// including batch, header, detail, payment schedule, and optional fields views.
    /// </summary>
    public class ApInvoiceBatchView
    {
        /// <summary>
        /// Gets or sets the control view for the AP invoice batch.
        /// Corresponding Accpac view number: "AP0020".
        /// </summary>
        public AccpacView BatchView { get; set; }

        /// <summary>
        /// Gets or sets the header view for the AP invoice batch.
        /// Corresponding Accpac view number: "AP0021".
        /// </summary>
        public AccpacView HeaderView { get; set; }

        /// <summary>
        /// Gets or sets the detail view for the AP invoice batch.
        /// Corresponding Accpac view number: "AP0022".
        /// </summary>
        public AccpacView DetailView { get; set; }

        /// <summary>
        /// Gets or sets the payment schedule view for the AP invoice batch.
        /// Corresponding Accpac view number: "AP0023".
        /// </summary>
        public AccpacView PaymentScheduleView { get; set; }

        /// <summary>
        /// Gets or sets the optional fields view for the header of the AP invoice batch.
        /// Corresponding Accpac view number: "AP0402".
        /// </summary>
        public AccpacView HeaderOptFieldView { get; set; }

        /// <summary>
        /// Gets or sets the optional fields view for the detail of the AP invoice batch.
        /// Corresponding Accpac view number: "AP0401".
        /// </summary>
        public AccpacView DetailOptFieldView { get; set; }
    }

}
