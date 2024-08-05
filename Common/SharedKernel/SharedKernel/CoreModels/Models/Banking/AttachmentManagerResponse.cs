using SharedKernel.CoreModels.LookupTable;
using SharedKernel.SqlScheme.Response;
using Newtonsoft.Json;

namespace SharedKernel.CoreModels.Models.Banking;

public class AttachmentManagerResponse : BaseResponse
{
    public string BankId { get; set; }
    public string BankAccountId { get; set; }
    public BankAccountMinimalResponse BankAccount { get; set; }
    public string AttachmentId { get; set; }
    public AttachmentFileTypeResponse AttachmentFileType { get; set; }
    public AttachmentStatusResponse AttachmentStatus { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string UpdatedBy { get; set; }
    public LookupTableBaseEntity? Currency { get; set; } 
    public LookupTableBaseEntity? Bank { get; set; } 
    public LookupTableBaseEntity? BankAccountType { get; set; }
    public string CompanyId { get; set; }
}