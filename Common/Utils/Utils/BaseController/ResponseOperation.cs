namespace Utils.BaseController;

public class ResponseOperation
{
    public ResponseOperation(Guid operationId, string operationName)
    {
        OperationId = operationId;
        OperationName = operationName;
    }

    public ResponseOperation(ResponseOperation responseOperation)
    {
        OperationId = responseOperation.OperationId;
        OperationName = responseOperation.OperationName;
    }

    public Guid OperationId { get; set; }
    public string OperationName { get; set; }
}