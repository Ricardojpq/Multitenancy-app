namespace Utils.BaseController;

public class Response403 : ResponseOperation
{
    public Response403(Guid operationId, string operationName, string message)
        : base(operationId, operationName)
    {
        Message = message;
    }

    public Response403(ResponseOperation responseOperation, string message)
        : base(responseOperation)
    {
        Message = message;
    }

    public string Message { get; set; }
}