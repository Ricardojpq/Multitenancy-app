namespace Utils.Shared
{
    public class ServiceResponse
    {
        public string EntityId { get; set; }
        public bool Success { get; set; }
        public string StatusMessage { get; set; }
        public int Count { get; set; }
        public string JsonEncodedDataResponse { get; set; }

        public ErrorType? ErrorTypeResponse { get; set; }

        public enum ErrorType
        {
            InternalError,
            BusinessError
        }



        /// <summary>
        /// Initializes a new service response with success default values
        /// </summary>
        public ServiceResponse()
        {
            this.EntityId = "";
            this.Count = 0;
            this.Success = true;
            this.StatusMessage = Constants.SUCCESS;
            this.ErrorTypeResponse = null;
        }
        public ServiceResponse(string jsonEncodedDataResponse)
        {
            this.EntityId = "";
            this.Count = 0;
            this.Success = true;
            this.StatusMessage = Constants.SUCCESS;
            this.ErrorTypeResponse = null;
            JsonEncodedDataResponse = jsonEncodedDataResponse;
        }
        public ServiceResponse(string jsonEncodedDataResponse, bool isErrorResponse, ErrorType? errorType)
        {
            this.EntityId = "";
            this.Count = 0;
            this.Success = isErrorResponse ? false : true;
            this.StatusMessage = isErrorResponse ? Constants.ERROR : Constants.SUCCESS;
            this.ErrorTypeResponse = errorType;
            JsonEncodedDataResponse = jsonEncodedDataResponse;
        }

        /// <summary>
        /// Initializes a new service response with success default values
        /// </summary>
        /// <param name="isErrorResponse">true for an error response, false for a success response</param>
        public ServiceResponse(bool isErrorResponse)
        {
            this.EntityId = isErrorResponse ? "errorResponse" : "";
            this.StatusMessage = isErrorResponse ? Constants.ERROR : Constants.SUCCESS;
            this.Success = isErrorResponse ? false : true;
        }

        /// <summary>
        /// Initializes a new service response with success default values
        /// </summary>
        /// <param name="entityId">The Id of the modified entity</param>
        /// <param name="message">The desired message to return</param>
        /// <param name="isErrorResponse">Whether the operation completed successfully or not</param>
        public ServiceResponse(string entityId = "", string message = Constants.SUCCESS, bool isErrorResponse = false, ErrorType errorType = ErrorType.InternalError)
        {
            this.EntityId = entityId;
            this.StatusMessage = message;

            if (isErrorResponse)
            {
                this.Success = false;
                this.ErrorTypeResponse = errorType;
            }
            else
            {
                this.Success = true;
            }
        }


        /// <summary>
        /// Initializes a new service response with success/error default values
        /// </summary>
        /// <param name="entityId">The Id of the modified entity</param>
        /// <param name="message">The desired message to return</param>
        /// <param name="isErrorResponse">Whether the operation completed successfully or not</param>
        public ServiceResponse(ErrorType errorResponse = ErrorType.InternalError, string entityId = "", string message = Constants.SUCCESS, bool isErrorResponse = false)
        {
            this.EntityId = entityId;
            this.StatusMessage = message;

            if (isErrorResponse)
            {
                this.Success = false;
                this.ErrorTypeResponse = errorResponse;
            }
            else
            {
                this.Success = true;
            }
        }

        /// <summary>
        /// Initializes a new service response with success default values
        /// </summary>
        /// <param name="message">The desired message to return</param>
        /// <param name="isErrorResponse">Whether the operation completed successfully or not</param>
        /// <param name="count">The count of items that were created</param>
        public ServiceResponse(bool isErrorResponse = false, int count = 0, string message = Constants.SUCCESS)
        {
            this.Count = count;
            this.StatusMessage = message;

            if (isErrorResponse)
            {
                this.Success = false;
                this.ErrorTypeResponse = ErrorType.InternalError;
            }
            else
            {
                this.Success = true;
            }
        }

    }
}
