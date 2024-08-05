using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SharedKernel.MongoScheme
{
    /// <summary>
    /// This class contains all the fields that every table in the database must have. Fields are:
    /// <para>Id</para>
    /// <para>IsActive</para>
    /// <para>CreateDate</para>
    /// <para>CreatedBy</para>
    /// </summary>
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid _Id { get; set; }

        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        [MaxLength(80)]
        public string CreatedBy { get; set; }
       
        public DateTime? UpdatedDate { get; set; }
        
        [MaxLength(80)]
        public string? UpdatedBy { get; set; }
        
        [Required]
        [BsonRepresentation(BsonType.String)]
        public Guid TenantId { get; set; }
        public bool IsDeleted { get; set; }
        public BaseEntity()
        {
            _Id = Guid.NewGuid();
            IsActive = true;
            CreatedDate = DateTime.Now.ToUniversalTime();
            CreatedBy = "";
	        IsDeleted = false;
            TenantId = Guid.NewGuid();
            UpdatedDate = DateTime.Now.ToUniversalTime();
            UpdatedBy = null;
        }

        public override string ToString()
        {
            return $"Entity Info: PK: {_Id}, TenantId:{TenantId}, Active: {IsActive}, Created: {CreatedDate}";
        }
    }
}
