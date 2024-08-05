using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedKernel.SqlScheme;

/// <summary>
/// This class contains all the fields that every table in the database must have. Fields are:
/// <para>Id</para>
/// <para>IsActive</para>
/// <para>CreateDate</para>
/// <para>CreatedBy</para>
/// </summary>
public class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    [Required] 
    public bool IsActive { get; set; }
    [Required] 
    public DateTime CreatedDate { get; set; }
    [Required] 
    [MaxLength(80)] 
    public string CreatedBy { get; set; }
    public bool IsDeleted { get; set; }     
    public DateTime? UpdatedDate { get; set; }
    [MaxLength(80)] 
    public string? UpdatedBy { get; set; }
    [Required]
    public Guid TenantId { get; set; }
    
    [Timestamp]
    public byte[]? TimeStamp { get; set; }
    
    public Guid CompanyId { get; set; }
    public BaseEntity()
    {
        Id = new Guid();
        IsActive = true;
        CreatedDate = DateTime.Now.ToUniversalTime();
        CreatedBy = "";
        IsDeleted = false;
        UpdatedDate = DateTime.Now.ToUniversalTime();
        UpdatedBy = null;
    }

    public override string ToString()
    {
        return $"Entity Info: PK: {Id},  Active: {IsActive}, Created: {CreatedDate}, CompanyId: {CompanyId}";
    }
}