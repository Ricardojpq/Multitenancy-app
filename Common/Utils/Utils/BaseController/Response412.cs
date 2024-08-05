namespace Utils.BaseController;

public class Response412 : ResponseOperation
{
    public Response412(Guid operationId, string operationName, string message, IEnumerable<Validation> validation)
        : base(operationId, operationName)
    {
        Message = message;
        Validation = validation;
    }

    public Response412(ResponseOperation responseOperation, string message, IEnumerable<Validation> validation)
        : base(responseOperation)
    {
        Message = message;
        Validation = validation;
    }

    public string Message { get; set; }
    public IEnumerable<Validation> Validation { get; set; }
}