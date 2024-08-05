using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Utils.Shared;
using ViewModels.Shared;

namespace ViewModels
{
    public abstract class EntityViewModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        [MaxLength(80)]
        public string MaintenanceUser { get; set; }

        public abstract string InternalIdentifier { get; }
    }
    public class BaseViewModel : EntityViewModel
    {
        [CustomRequired]
        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public override string InternalIdentifier => $"{{{Id}}}";

        public override string ToString()
        {
            return $"Id: {Id}, Active: {IsActive}, Created: {CreatedDate}";
        }

        public BaseViewModel()
        {
            MaintenanceUser = "User";
            CreatedDate = DateTime.Now;
        }
    }

    public class BaseFilterModel : BaseViewModel
    {
        [JsonIgnore]
        public IEnumerable<string> SearchableFields { get; }
        [JsonIgnore]
        public IEnumerable<string> SearchableTextFields { get; }
        [JsonIgnore]
        public IEnumerable<string> SearchableTypeFields { get; }
        [JsonIgnore]
        public IEnumerable<string> EntityRelations { get; }

        [JsonIgnore]
        public string EntityName { get; }
        [JsonIgnore]
        public string EntityColumnID { get; }
        [JsonIgnore]
        public string EntityColumnDescription { get; }

        [JsonIgnore]
        public ICollection<string> LookupEntityDetailsFields { get; }

        [JsonIgnore]
        public string EntityRelationName { get; }
        [JsonIgnore]
        public string EntityRelationForeignKey { get; }
        [JsonIgnore]
        public ICollection<string> LookupEntityRelationFields { get; }
    }

    public class DateRangeBaseViewModel : BaseViewModel
    {
        [CustomRequiredDateTime]
        [DataType(DataType.Date)]
        [Display(Name = "Effective Date")]
        public DateTime EffectiveDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, EffDate: {EffectiveDate}, ExpDate: {ExpirationDate}";
        }

        public string EffectiveDateString => EffectiveDate.ToString("MM/dd/yyyy");
        public string ExpirationDateString => ExpirationDate.HasValue ? ExpirationDate.Value.ToString("MM/dd/yyyy") : string.Empty;
    }

    public class LookupTableBaseViewModel : BaseViewModel
    {
        [CustomRequired]
        [MaxLength(80)]
        public string Name { get; set; }

        [CustomRequired]
        [Display(Name = "Description")]
        [MaxLength(256)]
        public string Description { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Desc: {Description}";
        }
    }

    public class LookupTableRangeViewModel : DateRangeBaseViewModel
    {
        [CustomRequired]
        [MaxLength(80)]
        public string Name { get; set; }

        [CustomRequired]
        [Display(Name = "Description")]
        [MaxLength(256)]
        public string Description { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Desc: {Description}";
        }
    }

    public class AddressBaseViewModel : BaseViewModel
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

    public class AddressDateRangeBaseEntity : DateRangeBaseViewModel
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

    public class AddressAndDemographicsBaseViewModel : BaseViewModel
    {
        [CustomRequired]
        [Display(Name = "First Name")]
        [MaxLength(35)]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [MaxLength(25)]
        public string MiddleName { get; set; }

        [CustomRequired]
        [Display(Name = "Last Name")]
        [MaxLength(80)]
        public string LastName { get; set; }

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
        [Display(Name = "County")]
        //[DataType(DataType.PostalCode)]
        [MaxLength(80)]
        public string County { get; set; }

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
        [Display(Name = "Country")]
        //[DataType(DataType.PostalCode)]
        [MaxLength(3)]
        public string Country { get; set; }


    }

    public class ClinicHistoryBaseViewModel : BaseViewModel
    {
        [CustomRequired]
        [MaxLength(80)]
        public string Name { get; set; }

        [CustomRequired]
        [Display(Name = "Description")]
        [MaxLength(500)]
        public string Description { get; set; }

    }

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

            if ((DateTime)value == default)
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

    public static class ViewModelExtensions
    {

        public static Type GetViewModelByName(this string viewModelName)
        {
            var type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == viewModelName);
            return type;
        }

        public static object GetViewModelInstance(this Type viewModelType)
        {
            var viewModelInstance = Activator.CreateInstance(viewModelType);

            return viewModelInstance;
        }

        public static Dictionary<string, string> GetDisplayNameProperties<T>(this T viewModelInstance, string propertyName)
        {
            var valuesList = ((Dictionary<string, string>)viewModelInstance.GetType().GetProperty(propertyName).GetValue(viewModelInstance)).Select(x => x.Key);

            Dictionary<string, string> searchable = new Dictionary<string, string>();

            foreach (var prop in valuesList)
            {
                MemberInfo property = viewModelInstance.GetType().GetProperty(prop);

                var customAttribute = property.GetCustomAttribute(typeof(DisplayAttribute)) ?? property.GetCustomAttribute(typeof(DisplayNameAttribute));

                var displayName = customAttribute == null ? prop : customAttribute.GetType().Name == nameof(DisplayNameAttribute) ? (customAttribute as DisplayNameAttribute).DisplayName : (customAttribute as DisplayAttribute).Name;

                searchable.Add(prop, displayName);
            }

            return searchable;
        }

        public static string GetViewModelPropertyValue(this object viewModelInstance, string propertyName)
        {
            var value = viewModelInstance.GetType().GetProperty(propertyName).GetValue(viewModelInstance).ToString();

            return value;
        }


        public static IEnumerable<EntityLookupVM> GetEntityLookupVMList(this IEnumerable<object> dataResult, Type viewModelType, object viewModelInstance)
        {
            List<EntityLookupVM> dataEntityList = new List<EntityLookupVM>();

            string columnId = viewModelInstance.GetViewModelPropertyValue(Constants.ViewModelEntityColumnId);
            string columnDescription = viewModelInstance.GetViewModelPropertyValue(Constants.ViewModelEntityColumnDescription);


            foreach (var element in dataResult)
            {
                JObject item = (JObject)element;

                var model = item.ToObject(viewModelType);

                EntityLookupVM entityLookupVM = new EntityLookupVM
                {
                    EntityColumnID = model.GetType().GetProperty(columnId).GetValue(model).ToString(),
                    EntityColumnDescription = model.GetType().GetProperty(columnDescription).GetValue(model).ToString()
                };

                dataEntityList.Add(entityLookupVM);
            }
            return dataEntityList;
        }
    }
}
