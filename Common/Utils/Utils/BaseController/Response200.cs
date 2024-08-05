using Newtonsoft.Json;

namespace Utils.BaseController;

public class Response200<T> : ResponseOperation
{
    public Response200(Guid operationId, string operationName, T data, Pagination pagination)
        : base(operationId, operationName)
    {
        Data = data;
        Pagination = pagination;
    }

    public Response200(ResponseOperation responseOperation, T data, Pagination pagination)
        : base(responseOperation)
    {
        Data = data;
        Pagination = pagination;
    }

    public Response200(ResponseOperation responseOperation, T data)
        : base(responseOperation)
    {
        Data = data;
    }

    public T Data { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public Pagination Pagination { get; set; }
}