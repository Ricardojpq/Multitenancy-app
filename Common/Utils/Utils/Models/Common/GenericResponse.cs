using Utils.BaseController;

namespace Utils.Models.Common
{
    public class GenericResponse
    {
        public GenericResponse()
        {
            Success = false;
            Data = null;
            ValidationErrors = new List<Validation>();
        }

        public GenericResponse(int statusCode, string errorMessage)
        {
            Success = false;
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
            Data = null;
            ValidationErrors = new List<Validation>();
        }

        public bool Success
        {
            get;
            set;
        }

        public object Data
        {
            get;
            set;
        }

        public int StatusCode
        {
            get;
            set;
        }

        #region Properties for Errors handling

        // error stack trace info
        public string StackTrace
        {
            get;
            set;
        }

        // error message (commonly used in BadRequest status code)
        public string ErrorMessage
        {
            get;
            set;
        }

        // Error list
        public List<Validation> ValidationErrors
        {
            get;
            set;
        }

        #endregion
    }

    public class GenericError
    {
        public string FieldName
        {
            get;
            set;
        }

        public string ErrorMessage
        {
            get;
            set;
        }

        public string FieldValue
        {
            get;
            set;
        }
    }
}
