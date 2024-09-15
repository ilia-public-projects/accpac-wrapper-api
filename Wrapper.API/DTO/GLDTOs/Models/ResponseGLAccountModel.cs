namespace Wrapper.API.DTO.GLDTOs.Models
{
    /// <summary>
    /// Represents a General Ledger (GL) account in the response.
    /// Contains the account code and a description of the account.
    /// </summary>
    public class ResponseGLAccountModel
    {
        /// <summary>
        /// Gets or sets the code of the GL account.
        /// </summary>
        /// <example>4000</example>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the description of the GL account.
        /// </summary>
        /// <example>Sales Revenue</example>
        public string Description { get; set; }
    }

}
