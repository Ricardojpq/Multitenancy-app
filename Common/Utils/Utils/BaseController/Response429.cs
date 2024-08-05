namespace Utils.BaseController;

public class Response429 : ResponseOperation
{
    public Response429(Guid operationId, string operationName, string message)
        : base(operationId, operationName)
    {
        Message = message;
    }

    public Response429(ResponseOperation responseOperation, string message)
        : base(responseOperation)
    {
        Message = message;
    }

    public string Message { get; set; }
}