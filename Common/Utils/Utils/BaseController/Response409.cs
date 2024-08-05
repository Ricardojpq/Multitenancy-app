namespace Utils.BaseController;

public class Response409 : ResponseOperation
{
    public Response409(Guid operationId, string operationName, string message)
        : base(operationId, operationName)
    {
        Message = message;
    }

    public Response409(ResponseOperation responseOperation, string message)
        : base(responseOperation)
    {
        Message = message;
    }

    public string Message { get; set; }
}