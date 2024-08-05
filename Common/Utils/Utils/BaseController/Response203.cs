namespace Utils.BaseController;

public class Response203<T> : ResponseOperation
{
    public Response203(Guid operationId, string operationName, string message, T alteration)
        : base(operationId, operationName)
    {
        Alteration = alteration;
        Message = message;
    }

    public Response203(ResponseOperation responseOperation, string message, T alteration)
        : base(responseOperation)
    {
        Alteration = alteration;
        Message = message;
    }

    public T Alteration { get; set; }
    public string Message { get; set; }
}