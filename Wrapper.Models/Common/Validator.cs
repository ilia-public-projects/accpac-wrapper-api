using Wrapper.Models.Common.Exceptions;

namespace Wrapper.Models.Common
{
    /// <summary>
    /// Provides functionality to collect validation errors and throw validation exceptions.
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Validator"/> class.
        /// </summary>
        public Validator()
        {
            Errors = new List<string>();
        }

        /// <summary>
        /// Gets or sets the list of validation errors.
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// Validates the current state by checking if there are any validation errors.
        /// Throws a <see cref="ValidationException"/> if any errors are found.
        /// </summary>
        /// <param name="message">An optional custom message to include in the exception.</param>
        /// <exception cref="ValidationException">Thrown when validation errors exist.</exception>
        public void Validate(string message = null)
        {
            if (Errors.Any())
            {
                throw new ValidationException(message ?? "Validation failed", Errors);
            }
        }

        /// <summary>
        /// Adds a validation error to the list of errors.
        /// </summary>
        /// <param name="message">The validation error message to add.</param>
        public void WriteError(string message)
        {
            Errors.Add(message);
        }

        /// <summary>
        /// Adds a validation error with an optional line number to the list of errors.
        /// </summary>
        /// <param name="message">The validation error message to add.</param>
        /// <param name="lineNo">The optional line number associated with the error.</param>
        public void WriteError(string message, int? lineNo)
        {
            if (lineNo.HasValue)
            {
                WriteLineError(lineNo.Value, message);
            }
            else
            {
                WriteError(message);
            }
        }

        /// <summary>
        /// Adds a validation error with an optional line number and immediately validates the state.
        /// If errors exist, throws a <see cref="ValidationException"/>.
        /// </summary>
        /// <param name="message">The validation error message to add.</param>
        /// <param name="lineNo">The optional line number associated with the error.</param>
        public void WriteErrorAndValidate(string message, int? lineNo)
        {
            WriteError(message, lineNo);
            Validate();
        }

        /// <summary>
        /// Adds a validation error with a specific line number to the list of errors.
        /// </summary>
        /// <param name="lineNo">The line number associated with the error.</param>
        /// <param name="message">The validation error message to add.</param>
        public void WriteLineError(int lineNo, string message)
        {
            Errors.Add($"Line no ({lineNo}), message: {message}");
        }
    }

}
