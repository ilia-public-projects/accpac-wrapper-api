namespace Wrapper.Models.Accpac.APModels.APSetupModels
{
    /// <summary>
    /// Represents a vendor in the Accounts Payable (AP) module, including the vendor's code, name, and group code.
    /// </summary>
    public class VendorModel
    {
        /// <summary>
        /// Gets or sets the unique code for the vendor.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the name of the vendor.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the group code associated with the vendor.
        /// </summary>
        public string GroupCode { get; set; }
    }

}
