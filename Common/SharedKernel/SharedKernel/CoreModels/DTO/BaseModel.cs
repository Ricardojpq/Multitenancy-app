namespace SharedKernel.CoreModels;

public class BaseModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? CreatedBy { get; set; }
    public bool IsDeleted { get; set; }     
    public DateTime? UpdatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
}