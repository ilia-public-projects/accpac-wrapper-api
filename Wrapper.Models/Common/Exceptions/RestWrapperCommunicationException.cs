using System.Runtime.Serialization;

namespace Wrapper.Models.Common.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs during communication in the <see cref="RestWrapper"/> when making REST requests.
    /// </summary>
    public class RestWrapperCommunicationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestWrapperCommunicationException"/> class.
        /// </summary>
        public RestWrapperCommunicationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestWrapperCommunicationException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public RestWrapperCommunicationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestWrapperCommunicationException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that caused the current exception.</param>
        public RestWrapperCommunicationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestWrapperCommunicationException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The serialization information that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The streaming context that contains contextual information about the source or destination.</param>
        protected RestWrapperCommunicationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}
