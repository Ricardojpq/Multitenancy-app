using System.ComponentModel.DataAnnotations;
using SharedKernel.MongoScheme;

namespace SharedKernel
{
    /// <summary>
    /// This class contains all the fields that a table with Address Information must have. Fields are:
    /// <para>Id</para>
    /// <para>IsActive</para>
    /// <para>CreateDate</para>
    /// <para>CreatedBy</para>
    /// <para>FirstName</para>
    /// <para>MiddleName</para>
    /// <para>LastName</para>
    /// <para>Address 1</para>
    /// <para>Address 2</para>
    /// <para>City</para>
    /// <para>County</para>
    /// <para>State</para>
    /// <para>ZipCode</para>
    /// <para>Country</para>
    /// </summary>
    public class AddressAndDemographicsBaseEntity : BaseEntity
    {
        [Required]
        [Display(Name = "First Name")]
        [MaxLength(35)]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [MaxLength(25)]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(80)]
        public string LastName { get; set; }

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
        [Display(Name = "County")]
        [MaxLength(80)]
        public string County { get; set; }

        [Required]
        [Display(Name = "State")]
        [MaxLength(2)]
        public string State { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        [DataType(DataType.PostalCode)]
        [MaxLength(15)]
        public string ZipCode { get; set; }

        [Required]
        [Display(Name = "Country")]
        [MaxLength(3)]
        public string Country { get; set; }
    }
}