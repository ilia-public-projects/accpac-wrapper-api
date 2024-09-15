using System.Runtime.Serialization;

namespace Wrapper.Models.Common.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when validation fails.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class with a specified error message and a list of validation errors.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="errors">The list of validation errors associated with the exception.</param>
        public ValidationException(string message, List<string> errors) : base(message)
        {
            Errors = errors;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ValidationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The serialization information object that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The streaming context object that contains contextual information about the source or destination.</param>
        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Gets the list of validation errors associated with this exception.
        /// </summary>
        public List<string> Errors { get; private set; }
    }

}
