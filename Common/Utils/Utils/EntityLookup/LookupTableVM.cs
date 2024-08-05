using System.Dynamic;
using System.Reflection;
using Utils.Extensions;
using Utils.Shared;

namespace Utils.EntityLookup
{
  public class LookupTableVM
  {
    public string Name { get; set; }
    public List<Dictionary<string, string>> Attributes { get; set; }
  }

  public static class LookupTableVMExtensions
  {
    public static object ConvertLookupTableVMToObject(this LookupTableVM lookupTableVM)
    {
      dynamic dynamicObject = lookupTableVM.ConvertLookupTableVMToDynamicObject();
      var type = TypeHelper.ReturnType(lookupTableVM.Name);
      var lookupTableRecord = (dynamicObject as ExpandoObject).ToLookupEntity(type);
      return lookupTableRecord;
    }
    public static dynamic ConvertLookupTableVMToDynamicObject(this LookupTableVM lookupTableVM)
    {
      var dynamicObject = new ExpandoObject() as IDictionary<string, Object>;
      foreach (var attribute in lookupTableVM.Attributes)
      {
        if (attribute.TryGetValue("ColumnName", out string name) && attribute.TryGetValue("Value", out string value) && attribute.TryGetValue("DataType", out string type))
        {
          object convertedValue = TypeHelper.ConvertStringToType(value, type);
          dynamicObject.Add(name, convertedValue);
        }
      }
      return dynamicObject;
    }
  }
  public static class TypeHelper
  {
    public static object ConvertStringToType(string value, string type)
    {
      Type typeCode = typeof(string);
      object valueObject = null;
      switch (type)
      {
        case "int":
          typeCode = typeof(int);
          valueObject = Convert.ChangeType(value, typeCode);
          break;
        case "bool":
          typeCode = typeof(Boolean);
          valueObject = Convert.ChangeType(value, typeCode);
          break;
        case "datetime":
          valueObject = DateTime.Parse(value);
          break;
        case "datetime?":
          typeCode = typeof(Nullable<DateTime>);
          if(DateTime.TryParse(value, out DateTime result))
            valueObject = result;
          break;
        case "Guid":
          valueObject = Guid.Parse(value);
          break;
        default:
          typeCode = typeof(string);
          valueObject = Convert.ChangeType(value, typeCode);
          break;
      }

      return valueObject;
    }
    public static Type ReturnType(string lookupTableName)
    {
      var typesByAssembly = Assembly.Load(Constants.DAL_Assembly).GetTypes();
      Type lookupTableType = typesByAssembly.FirstOrDefault(p => p.Name.Equals(lookupTableName));
      return lookupTableType;
    }
  }
  public static class ObjectHelper
  {
    public static bool TryMapProperties(object source, object destination)
    {
      Type sourceType = source.GetType();
      Type destinationType = destination.GetType();

      if (sourceType == destinationType)
      {
        var sourceProperties = sourceType.GetProperties();
        var destionationProperties = destinationType.GetProperties();

        var commonProperties = from sp in sourceProperties
                               join dp in destionationProperties on new { sp.Name, sp.PropertyType } equals
                                   new { dp.Name, dp.PropertyType }
                               select new { sp, dp };

        foreach (var match in commonProperties)
        {
          match.dp.SetValue(destination, match.sp.GetValue(source, null), null);
        }
        return true;
      }
      return false;
    }
    public static bool TryChangeBoolProperty(object source, string property, bool newValue)
    {
      Type sourceType = source.GetType();

      var sourceProperties = sourceType.GetProperties();

      var isActiveProperty = (from sp in sourceProperties
                              where sp.Name == property
                              select sp).FirstOrDefault();

      if (isActiveProperty != null)
      {
        isActiveProperty.SetValue(source, newValue, null);
        return true;
      }
      else
        return false;
    }
  }
}
