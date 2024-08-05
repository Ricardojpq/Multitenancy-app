using System.ComponentModel.DataAnnotations;

namespace SharedKernel.MongoScheme
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
        /// The desired value the dependent property must have, to make this property required
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
                    return new ValidationResult($"{validationContext.DisplayName} is required when {PropertyName} equals {value.ToString()}");
                }
            }
            return ValidationResult.Success;
        }
    }
}