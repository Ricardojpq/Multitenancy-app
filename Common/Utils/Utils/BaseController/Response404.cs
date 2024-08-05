namespace Utils.BaseController;

public class Response404 : ResponseOperation
{
    public Response404(Guid operationId, string operationName, string message)
        : base(operationId, operationName)
    {
        Message = message;
    }

    public Response404(ResponseOperation responseOperation, string message)
        : base(responseOperation)
    {
        Message = message;
    }

    public string Message { get; set; }
}