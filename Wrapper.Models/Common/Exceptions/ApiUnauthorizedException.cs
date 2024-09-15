using System.Runtime.Serialization;

namespace Wrapper.Models.Common.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when an API request is unauthorized.
    /// </summary>
    public class ApiUnauthorizedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiUnauthorizedException"/> class.
        /// </summary>
        public ApiUnauthorizedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiUnauthorizedException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ApiUnauthorizedException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiUnauthorizedException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that caused the current exception.</param>
        public ApiUnauthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiUnauthorizedException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The serialization information that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The streaming context that contains contextual information about the source or destination.</param>
        protected ApiUnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}
