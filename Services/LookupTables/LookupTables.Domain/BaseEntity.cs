using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookupTables.Domain
{
    public class BaseEntity : SharedKernel.MongoScheme.BaseEntity
    {
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        public BaseEntity()
        {
            CreatedDate = DateTime.UtcNow;
            CreatedBy = "";
        }

        public override string ToString()
        {
            return $"Entity Info: Id: {_Id}, Name: {Name}, Description: {Description}";
        }
    }
}
