namespace Wrapper.Models.Accpac.APModels.APSetupModels
{
    /// <summary>
    /// Represents the different locations where optional fields can be applied in the Accounts Payable (AP) module.
    /// </summary>
    public enum ApOptionalFieldLocation
    {
        /// <summary>
        /// Optional fields applied to vendors and vendor groups.
        /// </summary>
        VendorsAndVendorsGroups = 0,

        /// <summary>
        /// Optional fields applied to the remit-to location.
        /// </summary>
        RemitToLocation = 1,

        /// <summary>
        /// Optional fields applied to invoices.
        /// </summary>
        Invoices = 2,

        /// <summary>
        /// Optional fields applied to invoice details.
        /// </summary>
        InvoiceDetails = 3,

        /// <summary>
        /// Optional fields applied to payments.
        /// </summary>
        Payments = 4,

        /// <summary>
        /// Optional fields applied to adjustments.
        /// </summary>
        Adjustments = 5,

        /// <summary>
        /// Optional fields applied to revaluations.
        /// </summary>
        Revaluations = 6,
    }

}
