namespace Utils.BaseController;

public class Response422 : ResponseOperation
{
    private static IEnumerable<Error> ListToErrorMapper(IEnumerable<string> error)
    {
        List<Error> messages = error.Select(e => new Error() { Field = "field", Message = e }).ToList();
        return messages;
    }

    public Response422(Guid operationId, string operationName, string message, IEnumerable<string> error)
        : base(operationId, operationName)
    {
        IEnumerable<Error> messages = ListToErrorMapper(error);
        Message = message;
        Error = messages;
    }

    public Response422(ResponseOperation responseOperation, string message, IEnumerable<string> error)
        : base(responseOperation)
    {
        IEnumerable<Error> messages = ListToErrorMapper(error);
        Message = message;
        Error = messages;
    }

    public Response422(Guid operationId, string operationName, string message, IEnumerable<Error> error)
        : base(operationId, operationName)
    {
        Message = message;
        Error = error;
    }

    public Response422(ResponseOperation responseOperation, string message, IEnumerable<Error> error)
        : base(responseOperation)
    {
        Message = message;
        Error = error;
    }

    public string Message { get; set; }
    public IEnumerable<Error> Error { get; set; }
}