namespace Wrapper.Models.Accpac.GLModels.GLSetupModels
{
    /// <summary>
    /// Represents a General Ledger (GL) account, including the account code and description.
    /// </summary>
    public class GLAccountModel
    {
        /// <summary>
        /// Gets or sets the code of the GL account.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the description of the GL account.
        /// </summary>
        public string Description { get; set; }
    }

}
