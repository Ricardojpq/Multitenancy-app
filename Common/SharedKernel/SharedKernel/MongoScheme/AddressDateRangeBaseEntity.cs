using System.ComponentModel.DataAnnotations;

namespace SharedKernel.MongoScheme
{
    public class AddressDateRangeBaseEntity : DateRangeBaseEntity
    {
        [Required]
        [Display(Name = "Address 1")]
        [MaxLength(55)]
        public string Address1 { get; set; }

        [Display(Name = "Address 2")]
        [MaxLength(55)]
        public string Address2 { get; set; }

        [Required]
        [Display(Name = "City")]
        [MaxLength(30)]
        public string City { get; set; }

        [Required]
        [MaxLength(2)]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        [DataType(DataType.PostalCode)]
        [MaxLength(15)]
        public string ZipCode { get; set; }

        [Required]
        [MaxLength(3)]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [MaxLength(80)]
        [Display(Name = "County")]
        public string County { get; set; }
    }
}