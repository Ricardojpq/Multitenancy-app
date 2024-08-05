namespace Utils.BaseController;

public class Response406 : ResponseOperation
{
    public Response406(Guid operationId, string operationName, string message, IEnumerable<Header> header)
        : base(operationId, operationName)
    {
        Message = message;
        Header = header;
    }

    public Response406(ResponseOperation responseOperation, string message, IEnumerable<Header> header)
        : base(responseOperation)
    {
        Message = message;
        Header = header;
    }

    public Response406(ResponseOperation responseOperation, string message)
        : base(responseOperation)
    {
        Message = message;
    }

    public string Message { get; set; }
    public IEnumerable<Header> Header { get; set; }
}