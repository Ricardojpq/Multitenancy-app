﻿using Newtonsoft.Json;

namespace SharedKernel.SqlScheme.Response;

public class BaseResponse
{
    [JsonProperty("id")] public string Id { get; set; }
    [JsonProperty("name")]public string Name { get; set; }
    [JsonProperty("description")] public string Description { get; set; }
}