using Microsoft.AspNetCore.Http;

namespace SharedKernel.CoreModels.DTO.Banking;

public class BankDocumentDTO 
{
    public Guid CompanyId { get; set; }
    public Guid BankId { get; set; }
    public Guid BankAccountId { get; set; }
    public Guid AttachmentFileTypeId { get; set; }
    public IFormFile File { get; set; }
    public Guid AttachmentCategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class BankDocumentBase64DTO
{
    public string base64 { get; set; }
    public string fileName { get; set; }
}