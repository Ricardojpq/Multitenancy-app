namespace Utils.BaseController;

public class Response423 : ResponseOperation
{
    public Response423(Guid operationId, string operationName, string message)
        : base(operationId, operationName)
    {
        Message = message;
    }

    public Response423(ResponseOperation responseOperation, string message)
        : base(responseOperation)
    {
        Message = message;
    }

    public string Message { get; set; }
}