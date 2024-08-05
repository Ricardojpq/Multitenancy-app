using SharedKernel.MongoScheme;

namespace SharedKernel.CoreModels.LookupTable;

public class LookupTableBaseEntity
{
    public Guid _Id { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public Guid TenantId { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Code { get; set; }
    public string Symbol { get; set; }
}