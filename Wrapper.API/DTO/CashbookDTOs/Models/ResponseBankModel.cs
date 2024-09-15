namespace Wrapper.API.DTO.CashbookDTOs.Models
{
    /// <summary>
    /// Represents a bank model in the response, including information such as the bank code, name, currency, and active status.
    /// </summary>
    public class ResponseBankModel
    {
        /// <summary>
        /// Gets or sets the bank code, which is a unique identifier for the bank.
        /// </summary>
        /// <example>BANK001</example>
        public string BankCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the bank.
        /// </summary>
        /// <example>First National Bank</example>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the currency associated with the bank.
        /// </summary>
        /// <example>USD</example>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the bank is active.
        /// </summary>
        /// <example>true</example>
        public bool IsActive { get; set; }
    }

}
