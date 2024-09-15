using System.Runtime.Serialization;

namespace Wrapper.Models.Common.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when there is a failure to add an entity to the database.
    /// </summary>
    public class CreateEntityException : HandledExceptionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEntityException"/> class.
        /// </summary>
        public CreateEntityException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEntityException"/> class with a reference to the original exception.
        /// </summary>
        /// <param name="ex">The original exception that caused the error.</param>
        public CreateEntityException(Exception ex) : base(ex)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEntityException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public CreateEntityException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEntityException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public CreateEntityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEntityException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The serialization information that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The streaming context that contains contextual information about the source or destination.</param>
        protected CreateEntityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}
