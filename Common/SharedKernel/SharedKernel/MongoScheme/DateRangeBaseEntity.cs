using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedKernel.MongoScheme
{
    /// <summary>
    /// This class contains all the fields that tables with a Date Range must have. Fields are:
    /// <para>Id</para>
    /// <para>IsActive</para>
    /// <para>CreateDate</para>
    /// <para>CreatedBy</para>
    /// <para>EffectiveDate</para>
    /// <para>ExpirationDate</para>
    /// </summary>
    public class DateRangeBaseEntity : BaseEntity
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Effective Date")]
        public DateTime EffectiveDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        [NotMapped]
        public string EffectiveDateString => EffectiveDate.ToString("MM/dd/yyyy");

        [NotMapped]
        public string ExpirationDateString => ExpirationDate.HasValue ? ExpirationDate.Value.ToString("MM/dd/yyyy") : string.Empty;


        public override string ToString()
        {
            return $"Entity Info: Id: {_Id}, Effective Date: {EffectiveDate}, Expiration Date: {ExpirationDate}";
        }
    }
}