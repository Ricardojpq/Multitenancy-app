using Utils.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FileManagement.DTOs
{
    public class BaseDTO : EntityBaseDTO
    {
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }

    public class EntityBaseDTO
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        [MaxLength(80)]
        public string MaintenanceUser { get; set; }
        public EntityBaseDTO()
        {
            CreatedDate = DateTime.Now;
        }
    }

    public class DateRangeBaseDTO : BaseDTO
    {
        [CustomRequiredDateTime]
        [DataType(DataType.Date)]
        [Display(Name = "Effective Date")]
        public DateTime EffectiveDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Expiration Date")]
        public DateTime? ExpirationDate { get; set; }
        public string EffectiveDateString => EffectiveDate.ToString("MM/dd/yyyy");
        public string ExpirationDateString => ExpirationDate.HasValue ? ExpirationDate.Value.ToString("MM/dd/yyyy") : string.Empty;
    }

    public class LookupTableBaseDTO : BaseDTO
    {
        [CustomRequired]
        [MaxLength(80)]
        public string Name { get; set; }

        [CustomRequired]
        [Display(Name = "Description")]
        [MaxLength(256)]
        public string Description { get; set; }
    }

    public class AddressDateRangeBaseDTO : DateRangeBaseDTO
    {
        [CustomRequired]
        [Display(Name = "Address 1")]
        [MaxLength(55)]
        public string Address1 { get; set; }

        [Display(Name = "Address 2")]
        [MaxLength(55)]
        public string Address2 { get; set; }

        [CustomRequired]
        [Display(Name = "City")]
        [MaxLength(30)]
        public string City { get; set; }

        [CustomRequired]
        [Display(Name = "State")]
        [MaxLength(2)]
        public string State { get; set; }

        [CustomRequired]
        [Display(Name = "Zip Code")]
        [DataType(DataType.PostalCode)]
        [RegularExpression("^\\d{5}$|^\\d{9}$", ErrorMessage = "Invalid zip")]
        [MaxLength(15)]
        public string ZipCode { get; set; }

        [CustomRequired]
        [MaxLength(3)]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [MaxLength(80)]
        [Display(Name = "County")]
        public string County { get; set; }
    }



}
