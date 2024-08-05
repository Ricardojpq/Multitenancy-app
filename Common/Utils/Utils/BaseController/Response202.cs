namespace Utils.BaseController;

public class Response202 : ResponseOperation
{
    public Response202(Guid operationId, string operationName, string message, string id)
        : base(operationId, operationName)
    {
        Message = message;
        Id = id;
    }

    public Response202(ResponseOperation responseOperation, string message, string id)
        : base(responseOperation)
    {
        Message = message;
        Id = id;
    }

    public Response202(ResponseOperation responseOperation, string message, Guid id)
        : base(responseOperation)
    {
        Message = message;
        Id = id.ToString();
    }

    public string Message { get; set; }
    public string Id { get; set; }
}