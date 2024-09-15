namespace Wrapper.Models.Accpac.CashbookModels.CashbookSetupModels
{
    /// <summary>
    /// Represents a bank with its associated details.
    /// </summary>
    public class BankModel
    {
        /// <summary>
        /// Gets or sets the unique code for the bank.
        /// </summary>
        /// <value>The unique code for the bank.</value>
        public string BankCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the bank.
        /// </summary>
        /// <value>The name of the bank.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the currency used by the bank.
        /// </summary>
        /// <value>The currency used by the bank.</value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the bank is active.
        /// </summary>
        /// <value><c>true</c> if the bank is active; otherwise, <c>false</c>.</value>
        public bool IsActive { get; set; }
    }
}
