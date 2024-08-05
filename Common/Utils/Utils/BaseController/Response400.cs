namespace Utils.BaseController;

public class Response400 : ResponseOperation
{
    public Response400(Guid operationId, string operationName, string message, string exception)
        : base(operationId, operationName)
    {
        Message = message;
        Exception = exception;
    }

    public Response400(ResponseOperation responseOperation, string message, string exception)
        : base(responseOperation)
    {
        Message = message;
        Exception = exception;
    }

    public string Message { get; set; }
    public string Exception { get; set; }
}