using System.Runtime.Serialization;

namespace Wrapper.Models.Common.Exceptions
{
    /// <summary>
    /// Represents a base class for custom exceptions that are considered handled, providing additional context for handled errors.
    /// </summary>
    public class HandledExceptionBase : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HandledExceptionBase"/> class.
        /// </summary>
        public HandledExceptionBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HandledExceptionBase"/> class by wrapping an existing exception.
        /// The message from the provided exception is used in this instance.
        /// </summary>
        /// <param name="ex">The original exception to wrap.</param>
        public HandledExceptionBase(Exception ex) : base(ex.Message, ex)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HandledExceptionBase"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public HandledExceptionBase(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HandledExceptionBase"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that caused the current exception.</param>
        public HandledExceptionBase(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HandledExceptionBase"/> class with serialized data.
        /// </summary>
        /// <param name="info">The serialization information that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The streaming context that contains contextual information about the source or destination.</param>
        protected HandledExceptionBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}
