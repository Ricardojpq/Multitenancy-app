using System.Dynamic;
using System.Text;
using Utils.Shared;
using Newtonsoft.Json;

namespace Utils.Extensions
{
    public static class GenericTypeExtensions
    {
        public static dynamic ToLookupEntity(this ExpandoObject expandoObject, Type type)
        {
            var lookupTableRecord = Activator.CreateInstance(type);
            var dict = type.GetProperties().ToDictionary(p => p.Name.ToLower(), p => p);

            if (expandoObject != null)
            {

                var lookuptablePropertiesFromExpandoObject = expandoObject.ToList();
                foreach (var kv in lookuptablePropertiesFromExpandoObject)
                {
                    if (dict.TryGetValue(kv.Key.ToLower(), out var p))
                    {
                        var propType = p.PropertyType;

                        if (kv.Value != null)
                        {
                            //if (!propType.IsByRef && propType.Name != "Nullable`1")
                            //{
                            //    // Throw if type is a value type
                            //    // but not Nullable<>
                            //    throw new ArgumentException("not nullable");
                            //}
                            //if (kv.Value.GetType() != propType)
                            //{
                            //  // You could make this a bit less strict
                            //  // but I don't recommend it.
                            //  throw new ArgumentException("type mismatch");
                            //}
                        }
                        try
                        {
                            p.SetValue(lookupTableRecord, kv.Value, null);

                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                    }
                }

            }

            return lookupTableRecord;

        }
        public static List<dynamic> ToLookupEntities(this List<dynamic> expandoObjectList, Type type)
        {
            var entities = expandoObjectList.Select(x => (x as ExpandoObject).ToLookupEntity(type)).ToList();
            return entities;
        }


        public static StringContent JsonContent(this object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }


        //------------------------------------------------------------------------------------------------------------------------

        public static bool CheckDateOverlapping<T>(this T entity, IEnumerable<T> collection)
        {
            bool overlaps = false;

            if (entity != null)
            {
                var type = typeof(T);

                var properties = type.GetProperties();

                if (properties.Any(p => p.Name == Constants.PropertyEffectiveDate) && properties.Any(p => p.Name == Constants.PropertyExpirationDate))
                {
                    Dictionary<DateTime, DateTime?> ranges = new Dictionary<DateTime, DateTime?>();

                    foreach (var element in collection)
                    {
                        var effDate = (DateTime)properties.First(p => p.Name == Constants.PropertyEffectiveDate).GetValue(element);
                        var expDate = (DateTime?)properties.First(p => p.Name == Constants.PropertyExpirationDate).GetValue(element);

                        if (ranges.ContainsKey(effDate))
                            return true;

                        ranges.Add(effDate, expDate);
                    }

                    var entityEffDate = (DateTime)properties.First(p => p.Name == Constants.PropertyEffectiveDate).GetValue(entity);
                    var entityExpDate = (DateTime?)properties.First(p => p.Name == Constants.PropertyExpirationDate).GetValue(entity);

                    foreach (var range in ranges)
                    {
                        var startDate = range.Key;
                        var endDate = range.Value;

                        var effDateInRange = (startDate >= entityEffDate) && (!endDate.HasValue || entityEffDate <= endDate.Value || endDate == null) && (entityExpDate >= startDate);
                        var rangeOverlap = (entityEffDate <= startDate && (!entityExpDate.HasValue || entityExpDate.Value >= startDate))
                            || (entityEffDate >= startDate && (!endDate.HasValue || endDate.Value > entityExpDate));

                        var effDateEqEndDate = (!endDate.HasValue || entityEffDate <= endDate.Value) && (entityExpDate >= startDate || !entityExpDate.HasValue);

                        if (effDateInRange || rangeOverlap || effDateEqEndDate)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    throw new Exception($"The object of type {type.Name} does not contain one of the following properties: EffectiveDate or ExpirationDate");
                }
            }

            //return object with Success: true or false for overlap and Message: message indicating which date range conflicts with the <param> entity </param> date range
            return overlaps;
        }



        /// <summary>
        /// Validate if the dates entity to assign are between dates from entity source
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">New entity</param>
        /// <param name="entitySource">Entity assigned to new entity</param>
        /// <returns></returns>
        public static bool CheckValidDatesToAssign<T, V>(this T entity, V entitySource)
        {
            bool isValid = false;

            if (entity != null)
            {
                isValid = true;
                var type = typeof(T);
                var typeSource = typeof(V);

                var properties = type.GetProperties();
                var propertiesSource = typeSource.GetProperties();

                if ((properties.Any(p => p.Name == Constants.PropertyEffectiveDate) && properties.Any(p => p.Name == Constants.PropertyExpirationDate))
                    && (propertiesSource.Any(p => p.Name == Constants.PropertyEffectiveDate) && propertiesSource.Any(p => p.Name == Constants.PropertyExpirationDate)))
                {


                    var effDateSource = (DateTime)propertiesSource.First(p => p.Name == Constants.PropertyEffectiveDate).GetValue(entitySource);
                    var expDateSource = (DateTime?)propertiesSource.First(p => p.Name == Constants.PropertyExpirationDate).GetValue(entitySource);


                    var entityEffDate = (DateTime)properties.First(p => p.Name == Constants.PropertyEffectiveDate).GetValue(entity);
                    var entityExpDate = (DateTime?)properties.First(p => p.Name == Constants.PropertyExpirationDate).GetValue(entity);

                    //if (!(entityEffDate >= effDateSource && (entityExpDate.HasValue && expDateSource.HasValue ? entityExpDate <= expDateSource :
                    //                                         !expDateSource.HasValue)))

                    if (effDateSource != null && expDateSource != null ? effDateSource <= expDateSource : effDateSource != null)
                    {
                        if (!(effDateSource >= entityEffDate && (entityExpDate.HasValue && expDateSource.HasValue ? expDateSource <= entityExpDate : !entityExpDate.HasValue)))
                        {
                            isValid = false;
                        }
                    }
                    else
                    {
                        isValid = false;
                    }
                }
                else
                {
                    throw new Exception($"The object of type {type.Name} does not contain one of the following properties: EffectiveDate or ExpirationDate");
                }
            }

            return isValid;
        }

        /// <summary>
        /// Check if Effective Date and Effective Date are valid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns>true if valid</returns>
        public static bool CheckValidDates<T>(this T entity)
        {
            bool isValid = false;

            if (entity != null)
            {
                var type = typeof(T);

                var properties = type.GetProperties();

                if (properties.Any(p => p.Name == Constants.PropertyEffectiveDate) && properties.Any(p => p.Name == Constants.PropertyExpirationDate))
                {

                    var entityEffDate = (DateTime)properties.First(p => p.Name == Constants.PropertyEffectiveDate).GetValue(entity);
                    var entityExpDate = (DateTime?)properties.First(p => p.Name == Constants.PropertyExpirationDate).GetValue(entity);
                    if ((entityExpDate >= entityEffDate && (entityExpDate.HasValue)) || entityExpDate == null)
                    {
                        isValid = true;
                    }
                }
                else
                {
                    throw new Exception($"The object of type {type.Name} does not contain one of the following properties: EffectiveDate or ExpirationDate");
                }
            }

            return isValid;
        }
    }
}
