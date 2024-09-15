using System.Runtime.Serialization;

namespace Wrapper.Models.Common.Exceptions
{
    /// <summary>
    /// Represents an exception specific to Accpac operations, typically used to handle errors during Accpac-related processes.
    /// </summary>
    public class AccpacException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccpacException"/> class with a specified error message and a list of Accpac-specific errors.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="errors">The list of Accpac-specific errors associated with the exception.</param>
        public AccpacException(string message, List<string> errors) : base(message)
        {
            Errors = errors;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccpacException"/> class.
        /// </summary>
        public AccpacException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccpacException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AccpacException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccpacException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that caused the current exception.</param>
        public AccpacException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccpacException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The serialization information that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The streaming context that contains contextual information about the source or destination.</param>
        protected AccpacException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Gets the list of Accpac-specific errors associated with this exception.
        /// </summary>
        public List<string> Errors { get; private set; }
    }

}
