
using SharedKernel.MongoScheme.Response;
using Newtonsoft.Json;

namespace SharedKernel.CoreModels.Models.Attachment;

public class AttachmentResponse : BaseResponse
{
    [JsonProperty("entityId")]
    public string EntityId { get; set; }

    [JsonProperty("attachmentCategory")]
    public AttachmentCategory AttachmentCategory { get; set; }

    [JsonProperty("identifier")]
    public string Identifier { get; set; }

    [JsonProperty("isActive")]
    public bool IsActive { get; set; }

    [JsonProperty("createdDate")]
    public DateTime CreatedDate { get; set; }

    [JsonProperty("createdBy")]
    public string CreatedBy { get; set; }

    [JsonProperty("updatedDate")]
    public object UpdatedDate { get; set; }

    [JsonProperty("updatedBy")]
    public string UpdatedBy { get; set; }

    [JsonProperty("tenantId")]
    public string TenantId { get; set; }

    [JsonProperty("isDeleted")]
    public bool IsDeleted { get; set; }
}

    public class Application : BaseResponse
    {
        [JsonProperty("bucketName")]
        public string BucketName { get; set; }
    }

    public class AttachmentCategory : BaseResponse
    {
        [JsonProperty("entityType")]
        public ResponseType ResponseType { get; set; }
    }

    public class ResponseType : BaseResponse
    {
        [JsonProperty("application")]
        public Application Application { get; set; }
    }



    