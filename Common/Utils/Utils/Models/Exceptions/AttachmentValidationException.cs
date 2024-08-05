using System.Runtime.Serialization;
using Utils.BaseController;
using Utils.Models.Common;

namespace Utils.Models.Exceptions;

[Serializable]
public class AttachmentValidationException : Exception
{
    private readonly GenericResponse _genericResponse;
    private const int HttpCode = 412;

    public AttachmentValidationException(string errorMessage, List<Validation> validationList)
    {
        _genericResponse = new GenericResponse
        {
            ErrorMessage = errorMessage,
            ValidationErrors = validationList,
            StatusCode = HttpCode
        };
    }

    public AttachmentValidationException(string message) : base(message)
    {
    }

    public AttachmentValidationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected AttachmentValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue("AttachmentValidationException.code", HttpCode);
        info.AddValue("AttachmentValidationException.genericResponse", _genericResponse);
    }

    public GenericResponse GenericResponse()
    {
        return _genericResponse;
    }
}