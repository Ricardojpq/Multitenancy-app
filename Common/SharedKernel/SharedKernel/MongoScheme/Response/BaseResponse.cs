using Newtonsoft.Json;

namespace SharedKernel.MongoScheme.Response;

public class BaseResponse
{
    [JsonProperty("_Id")]
    public string Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public object Description { get; set; }
}