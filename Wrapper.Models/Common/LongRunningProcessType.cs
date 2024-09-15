namespace Wrapper.Models.Common
{
    /// <summary>
    /// Defines the types of long-running processes that can be tracked for progress reporting.
    /// </summary>
    public enum LongRunningProcessType
    {
        /// <summary>
        /// Represents the posting of an Accounts Payable cashbook to Accpac.
        /// </summary>
        APcashbookAccpacPosting,

        /// <summary>
        /// Represents the posting of a Nominal cashbook to Accpac.
        /// </summary>
        NominalcashbookAccpacPosting,

        /// <summary>
        /// Represents the posting of an Accounts Payable invoice to Accpac.
        /// </summary>
        APinvoiceAccpacPosting
    }
}
