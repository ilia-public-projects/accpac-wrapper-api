using System.Runtime.Serialization;

namespace Wrapper.Models.Common.Exceptions
{
    /// <summary>
    /// Represents a generic exception that occurs within the <see cref="RestWrapper"/> class during REST request execution.
    /// </summary>
    public class RestWrapperException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestWrapperException"/> class.
        /// </summary>
        public RestWrapperException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestWrapperException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public RestWrapperException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestWrapperException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that caused the current exception.</param>
        public RestWrapperException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestWrapperException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The serialization information that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The streaming context that contains contextual information about the source or destination.</param>
        protected RestWrapperException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}
