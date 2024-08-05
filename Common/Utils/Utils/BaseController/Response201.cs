namespace Utils.BaseController;

public class Response201<T> : ResponseOperation
{
    public Response201(Guid operationId, string operationName, T data)
        : base(operationId, operationName)
    {
        Data = data;
    }

    public Response201(ResponseOperation responseOperation, T data)
        : base(responseOperation)
    {
        Data = data;
    }

    public T Data { get; set; }
}