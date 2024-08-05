using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Utils.Shared
{
    /// <summary>
    /// Custom Attribute to make a property required depending on the value of another property
    /// </summary>
    public class RequiredIfAttribute : RequiredAttribute
    {
        /// <summary>
        /// The dependent property name
        /// </summary>
        private string PropertyName { get; set; }

        /// <summary>
        /// The deisred value the dependent property must have, to make this property required
        /// </summary>
        private object DesiredValue { get; set; }

        /// <summary>
        /// Whether it should be equals or distinct to the desired value. The default value is equal (false)
        /// </summary>
        private bool Invert { get; set; }

        /// <summary>
        /// Makes the current property depend on another property value
        /// </summary>
        /// <param name="propertyName">The name of the dependent property</param>
        /// <param name="value">The deisred value the dependent property must have, to make this property required</param>
        /// <param name="different">Whether the dependent property should be equals or distinct to the desired value. The default value is equals (false)</param>
        public RequiredIfAttribute(string propertyName, object value, bool different = false)
        {
            PropertyName = propertyName;
            DesiredValue = value;
            Invert = different;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            object instance = validationContext.ObjectInstance;
            var type = instance.GetType();

            object propertyvalue = type.GetProperty(PropertyName).GetValue(instance, null);

            if (Invert == true)
            {
                if (propertyvalue.ToString() != DesiredValue.ToString())
                {
                    return new ValidationResult($"{validationContext.DisplayName} is required when {PropertyName} has a value different than {value.ToString()}.");
                }

            }
            else
            {
                if (propertyvalue.ToString() == DesiredValue.ToString())
                {
                    if (value is null)
                        return new ValidationResult($"{validationContext.DisplayName} is required when {PropertyName} equals {propertyvalue.ToString()}");
                }
            }

            return ValidationResult.Success;
        }
    }

    public class CustomRequiredAttribute : RequiredAttribute
    {
        public CustomRequiredAttribute()
        {
            ErrorMessage = "Required Field.";
        }
    }

    public class CustomRequiredDateTimeAttribute : RequiredAttribute
    {
        public CustomRequiredDateTimeAttribute()
        {
            ErrorMessage = "Required Field.";
        }

        public override bool IsValid(object value)
        {
            if (!(value is DateTime))
                throw new ArgumentException("Value must be a DateTime object");

            if ((DateTime)value == DateTime.MinValue)
                return false;

            if ((DateTime)value == default(DateTime))
                return false;

            return true;
        }
    }

    public class MustHaveOneElementAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as IList;

            if (list != null)
            {
                return list.Count > 0;
            }
            return false;
        }
    }

    public class Isa01QualifierAllowedAttribute : RequiredAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.ToString() == "00")
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Please enter a correct value. Only 00 is supported");
        }
    }

    public class Isa03QualifierAllowedAttribute : RequiredAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.ToString() == "00" || value.ToString() == "01")
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Please enter a correct value. Only 00 and 01 are supported");
        }
    }

    public class IsaIDQualifierAllowedAttribute : RequiredAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.ToString() == "01" || value.ToString() == "14" || value.ToString() == "20" ||
                value.ToString() == "27" || value.ToString() == "28" || value.ToString() == "29" || value.ToString() == "30" ||
                value.ToString() == "33" || value.ToString() == "ZZ")
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Please enter a correct value. Only 01,14,20,27,28,29,30,33 and ZZ are supported");
        }
    }

}
