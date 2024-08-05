using System.ComponentModel.DataAnnotations;

namespace SharedKernel.MongoScheme
{
    public class DateGreaterThanOrEqualAttribute : ValidationAttribute
    {
        public DateGreaterThanOrEqualAttribute(string dateToCompareToFieldName)
        {
            DateToCompareFieldName = dateToCompareToFieldName;
        }

        private string DateToCompareFieldName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;
            DateTime? greaterDate = (DateTime?)value;
            var propertyToValidate = validationContext.ObjectType.GetProperty(DateToCompareFieldName);
            DateTime earlierDate = (DateTime)propertyToValidate.GetValue(validationContext.ObjectInstance, null);

            if (!greaterDate.HasValue || (greaterDate.HasValue && greaterDate.Value >= earlierDate))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"{validationContext.MemberName} is not greater than {DateToCompareFieldName}");
            }
        }
    }
}