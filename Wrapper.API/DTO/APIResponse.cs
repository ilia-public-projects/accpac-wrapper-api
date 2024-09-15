namespace Wrapper.API.DTO
{
    public class APIResponse<T>
    {
        /// <summary></summary>
        public APIResponse(T response)
        {
            Response = response;
        }

        /// <summary></summary>
        public APIResponse(string[] errorMessages)
        {
            ErrorMessages = errorMessages;
        }

        /// <summary></summary>
        public APIResponse(T response, string[] errorMessages)
        {
            Response = response;
            ErrorMessages = errorMessages;
        }

        /// <summary></summary>
        public string[] ErrorMessages { get; private set; }
        /// <summary></summary>
        public T Response { get; private set; }
    }
}
