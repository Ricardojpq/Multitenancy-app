using System.Runtime.Serialization;
using Utils.BaseController;
using Utils.Models.Common;

namespace Utils.Models.Exceptions;

[Serializable]
public class BankDocumentValidationException : Exception
{
    private readonly GenericResponse _genericResponse;
    private const int HttpCode = 412;

    public BankDocumentValidationException(string errorMessage, List<Validation> validationList)
    {
        _genericResponse = new GenericResponse
        {
            ErrorMessage = errorMessage,
            ValidationErrors = validationList,
            StatusCode = HttpCode
        };
    }

    public BankDocumentValidationException(string message) : base(message)
    {
    }

    public BankDocumentValidationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected BankDocumentValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue("BankDocumentValidationException.code", HttpCode);
        info.AddValue("BankDocumentValidationException.genericResponse", _genericResponse);
    }

    public GenericResponse GenericResponse()
    {
        return _genericResponse;
    }
}