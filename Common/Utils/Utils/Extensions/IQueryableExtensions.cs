using System.Dynamic;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using Utils.Shared;
using Newtonsoft.Json.Linq;

namespace Utils.Extensions
{
    public static class IQueryableExtensions
    {
        public static IEnumerable<object> TryFilterJObject(this IEnumerable<object> collection,
            string property_name, string value)
        {
            var predicateFunction = new Func<Object, bool>((Object obj) =>
            {
                JObject jObject = (JObject)obj;
                foreach (KeyValuePair<string, JToken> keyValuePair in jObject)
                {
                    if (string.Equals(property_name, keyValuePair.Key, StringComparison.OrdinalIgnoreCase))
                    {
                        if (string.Equals(value, keyValuePair.Value.ToString(), StringComparison.OrdinalIgnoreCase)) return true;
                    }
                }
                return false;
            });
            return collection.Where(x => predicateFunction(x));
        }

        public static IEnumerable<T> TryFilterJObjectToDataTable<T>(this IEnumerable<T> collection,
            string value)
        {
            Dictionary<string, bool> dicNotKeyValues = new Dictionary<string, bool>
            {
              { Constants.Lookup_Table_IsActive, false },
              { Constants.Lookup_Table_UpdatedBy, false},
              { Constants.Lookup_Table_CreatedDate, false}
            };

            var predicateFunction = new Func<Object, bool>((Object obj) =>
            {
                var objProperties = obj.GetType().GetProperties().ToDictionary(p => p.Name);
                if (objProperties.ContainsKey(Constants.PropertyEffectiveDate) || objProperties.ContainsKey(Constants.PropertyExpirationDate))
                {
                    PropertyInfo? effectiveDateProperty = obj.GetType().GetProperty(Constants.PropertyEffectiveDate);
                    PropertyInfo? expirationDateProperty = obj.GetType().GetProperty(Constants.PropertyExpirationDate);
                    DateTime? effectiveDateValue = Convert.ToDateTime(effectiveDateProperty!.GetValue(obj, null));
                    DateTime? expirationDateValue = Convert.ToDateTime(effectiveDateProperty.GetValue(obj, null));
                    if (effectiveDateValue.HasValue
                        &&
                        (
                            effectiveDateValue.Value.ToString(CultureInfo.InvariantCulture).IndexOf(value) >= 0
                            ||
                            expirationDateValue.Value.ToString(CultureInfo.InvariantCulture).IndexOf(value) >= 0)
                        )
                        return true;
                }
                foreach (KeyValuePair<string, PropertyInfo> keyValuePair in objProperties)
                {
                    if (!dicNotKeyValues.ContainsKey(keyValuePair.Key))
                    {
                        var objValue = keyValuePair.Value.GetValue(obj);
                        if (objValue != null && objValue.ToString().IndexOf(value, StringComparison.CurrentCultureIgnoreCase) >= 0) return true;
                    }
                }
                return false;
            });
            return collection.Where(x => predicateFunction(x));
        }

        public static IEnumerable<object> TryFilter(this IEnumerable<object> collection,
            string property_name, string value)
        {
            var objTypeDictionary = new Dictionary<Type, PropertyInfo>();

            var predicateFunc = new Func<Object, String, String, bool>((obj, propName, propValue) =>
            {
                var objType = obj.GetType();
                PropertyInfo property = null;
                if (!objTypeDictionary.ContainsKey(objType))
                {
                    property = objType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(prop => prop.Name == propName);
                    objTypeDictionary[objType] = property;
                }
                else
                {
                    property = objTypeDictionary[objType];
                }

                if (property != null && property.GetValue(obj, null).ToString().ToUpper() == propValue.ToUpper())
                    return true;

                Type myType = obj.GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

                foreach (PropertyInfo prop in props)
                {
                    object propValue1 = prop.GetValue(obj, null);
                }

                return false;
            });
            var filterCollection = collection.Where(obj => predicateFunc(obj, property_name, value)).Instance();
            return filterCollection;
        }

        public static IEnumerable<T> TryFilterEffectiveRecord<T>(this IEnumerable<T> collection)
        {


            var objTypeDictionary = new Dictionary<string, PropertyInfo>();

            var predicateFunc = new Func<Object, string, string, DateTime, bool>((obj, propEffDate, propExpDate, propValue) =>
            {
                var objType = obj.GetType();

                PropertyInfo propertyEffDate = null;
                PropertyInfo propertyExpDate = null;

                if (!objTypeDictionary.ContainsKey(propEffDate))
                {
                    propertyEffDate = objType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(prop => prop.Name == propEffDate);
                    objTypeDictionary[propEffDate] = propertyEffDate;
                }
                else
                {
                    propertyEffDate = objTypeDictionary[propEffDate];
                }

                if (!objTypeDictionary.ContainsKey(propExpDate))
                {
                    propertyExpDate = objType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(prop => prop.Name == propExpDate);
                    objTypeDictionary[propExpDate] = propertyExpDate;
                }
                else
                {
                    propertyExpDate = objTypeDictionary[propExpDate];
                }

                if (propertyEffDate != null && propValue > DateTime.Parse(propertyEffDate.GetValue(obj, null).ToString()))
                    if (propertyExpDate == null || propertyExpDate.GetValue(obj, null) == null || (propertyExpDate != null && propValue < DateTime.Parse(propertyExpDate.GetValue(obj, null).ToString())))
                        return true;

                return false;
            });


            return collection.Where(obj => predicateFunc(obj, Constants.PropertyEffectiveDate, Constants.PropertyExpirationDate, DateTime.Now));

        }


        public static IEnumerable<T> OrderByDynamic<T>(this IEnumerable<T> collection, string sortColumn, bool ascending = true)
        {

            Type t = collection != null && collection.Count() > 0 ? collection.ToList()[0].GetType() : null;

            if (t != null)
            {
                IOrderedEnumerable<T> sortedCollection = null;

                if (ascending)
                {
                    sortedCollection = collection.OrderBy(
                        a => t.InvokeMember(
                            sortColumn
                            , BindingFlags.GetProperty
                            , null
                            , a
                            , null
                        )
                    );
                }

                else
                {
                    sortedCollection = collection.OrderByDescending(
                        a => t.InvokeMember(
                            sortColumn
                            , BindingFlags.GetProperty
                            , null
                            , a
                            , null
                        )
                    );

                }

                return sortedCollection;
            }

            return collection;
        }

        private static IEnumerable<T> OrderByProperty<T, P>(this IEnumerable<T> query, ParameterExpression prm, Expression property, bool ascending)
        {
            Func<IEnumerable<T>, Func<T, P>, IEnumerable<T>> orderBy = (q, p) => ascending ? q.OrderBy(p) : q.OrderByDescending(p);

            return orderBy(query, Expression.Lambda<Func<T, P>>(property, prm).Compile());
        }

        public static List<object> CompareExpandoObjectListToObjectListWithProperty(this List<dynamic> currentList, List<object> targetList, string property)
        {
            List<object> matchedList = new List<object>();
            foreach (var currentObject in currentList)
            {
                foreach (var currentTarget in targetList)
                {
                    if (((currentObject) as ExpandoObject).CompareAndReplaceObjectWithProperty(currentTarget, property, out object replacedPropertiesObject))
                        matchedList.Add(replacedPropertiesObject);
                }
            }
            return matchedList;
        }

        private static bool CompareAndReplaceObjectWithProperty(this ExpandoObject currentObject, object targetObject, string propertyToCompare, out object replacedPropertiesObject)
        {
            replacedPropertiesObject = null;
            if (ReferenceEquals(currentObject, targetObject)) return true;
            if ((currentObject == null) || (targetObject == null)) return false;
            var property = targetObject.GetType().GetProperty(propertyToCompare);

            var result = true;

            var currentValue = (currentObject as IDictionary<string, object>)[propertyToCompare];
            var targetValue = property.GetValue(targetObject);

            if (!currentValue.Equals(targetValue))
                result = false;
            else
                replacedPropertiesObject = ReplaceObjectProperties(currentObject, targetObject);

            return result;
        }

        private static object ReplaceObjectProperties(this ExpandoObject sourceObject, object targetObject)
        {
            var properties = targetObject.GetType().GetProperties();
            foreach (var property in properties)
            {
                if ((sourceObject as IDictionary<string, object>).Keys.Contains(property.Name))
                    property.SetValue(targetObject, (sourceObject as IDictionary<string, object>)[property.Name]);
            }
            return targetObject;
        }



        public static IEnumerable<object> TryFilterList(this IEnumerable<object> collection, string property_name, List<string> values)
        {
            var objTypeDictionary = new Dictionary<Type, PropertyInfo>();

            var predicateFunc = new Func<Object, String, List<string>, bool>((obj, propName, propValues) =>
            {
                var objType = obj.GetType();
                PropertyInfo property = null;
                if (!objTypeDictionary.ContainsKey(objType))
                {
                    property = objType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(prop => prop.Name == propName);
                    objTypeDictionary[objType] = property;
                }
                else
                {
                    property = objTypeDictionary[objType];
                }

                if (property != null && propValues.Contains(property.GetValue(obj, null).ToString()))
                    return true;

                Type myType = obj.GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

                foreach (PropertyInfo prop in props)
                {
                    object propValue1 = prop.GetValue(obj, null);
                }

                return false;
            });
            var filterCollection = collection.Where(obj => predicateFunc(obj, property_name, values)).Instance();
            return filterCollection;
        }
        public static IQueryable<T> ExpressionTreeSearch<T>(this IQueryable<T> data, List<string> properties, List<string> values)
        {
            for (var i = 0; i < properties.Count; i++)
            {
                var lambda = ConvertFilterToExpression<T>(properties[i], values[i]);
                data = data.Where(lambda);
            }
            return data;
        }
        public static Expression<Func<T, bool>> ConvertFilterToExpression<T>(string propToSearch,
   string valueToSearch)
        {

            var toStringMethod = typeof(object).GetMethod("ToString");
            var containsMethod = typeof(string).GetMethods().Where(x => x.Name == "Contains").First(x => !x.ContainsGenericParameters);
            var concatMethod = typeof(string).GetMethods(BindingFlags.Static | BindingFlags.Public).Where(m => m.Name == nameof(String.Concat) && m.GetParameters().All(x => x.ParameterType == typeof(string)) && m.GetParameters().Length == 2).FirstOrDefault();

            var parameter = Expression.Parameter(typeof(T), "record");

            MethodCallExpression fullNameExpresion = null;
            if (typeof(T).HasProperty("FirstName") && typeof(T).HasProperty("LastName"))
                fullNameExpresion = Expression.Call(concatMethod, Expression.Call(concatMethod, Expression.PropertyOrField(parameter, "FirstName"), Expression.Constant(" ")), Expression.PropertyOrField(parameter, "LastName"));

            // filtering is not required
            if (string.IsNullOrEmpty(propToSearch) || string.IsNullOrEmpty(valueToSearch))
                return Expression.Lambda<Func<T, bool>>(Expression.Constant(true), parameter);

            string dateToSearch = ConvertDateToSQLFormat(valueToSearch);
            var propertyExpression = Expression.PropertyOrField(parameter, propToSearch);
            var body = propertyExpression.Type == typeof(string)
                    ? propertyExpression.Member.Name == "FullName" ? Expression.Call(fullNameExpresion, containsMethod, Expression.Call(Expression.Constant(valueToSearch), toStringMethod)) : Expression.Call(propertyExpression, containsMethod, Expression.Call(Expression.Constant(valueToSearch), toStringMethod))
                    : propertyExpression.Type == typeof(DateTime)
                        ? Expression.Call(Expression.Call(propertyExpression, toStringMethod), containsMethod, Expression.Call(Expression.Constant(dateToSearch), toStringMethod))
                        : Expression.Call(Expression.Call(propertyExpression, toStringMethod), containsMethod, Expression.Call(Expression.Constant(valueToSearch), toStringMethod));

            var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
            return lambda;
        }
        public static IQueryable<T> ExpressionTreeSearch<T>(this IQueryable<T> data, string value) where T : new()
        {
            var instance = new T();
            var properties = instance.GetType().GetProperty(Constants.SearchableFields);
            var lambda = ConvertFilterToExpression<T>(properties.GetValue(instance) as List<string>, value);
            data = data.Where(lambda);
            return data;
        }
        public static IQueryable<T> ExpressionTreeSearch<T>(this IQueryable<T> data, List<string> properties, string value)
        {
            var lambda = ConvertFilterToExpression<T>(properties, value);
            data = data.Where(lambda);
            return data;
        }
        public static Expression<Func<T, bool>> ConvertFilterToExpression<T>(List<string> propsToSearch,
   string valueToSearch)
        {

            var toStringMethod = typeof(object).GetMethod("ToString");
            var containsMethod = typeof(string).GetMethods().Where(x => x.Name == "Contains").First(x => !x.ContainsGenericParameters);
            var concatMethod = typeof(string).GetMethods(BindingFlags.Static | BindingFlags.Public).Where(m => m.Name == nameof(String.Concat) && m.GetParameters().All(x => x.ParameterType == typeof(string)) && m.GetParameters().Length == 2).FirstOrDefault();

            var parameter = Expression.Parameter(typeof(T), "record");

            MethodCallExpression fullNameExpresion = null;
            if (typeof(T).HasProperty("FirstName") && typeof(T).HasProperty("LastName"))
                fullNameExpresion = Expression.Call(concatMethod, Expression.Call(concatMethod, Expression.PropertyOrField(parameter, "FirstName"), Expression.Constant(" ")), Expression.PropertyOrField(parameter, "LastName"));

            // filtering is not required
            if (!propsToSearch.Any() || string.IsNullOrEmpty(valueToSearch))
                return Expression.Lambda<Func<T, bool>>(Expression.Constant(true), parameter);

            string dateToSearch = ConvertDateToSQLFormat(valueToSearch);
            var props = typeof(T).GetProperties()
                    .Select(p => p.Name)
                    .Intersect(propsToSearch.Distinct());
            var body = props
                .Select(p => Expression.PropertyOrField(parameter, p))
                .Aggregate((Expression)Expression.Constant(false),
                    (c, n) => Expression.OrElse(c,
                        n.Type == typeof(string)
                    ? n.Member.Name == "FullName" ? Expression.Call(fullNameExpresion, containsMethod, Expression.Call(Expression.Constant(valueToSearch), toStringMethod)) : Expression.Call(n, containsMethod, Expression.Call(Expression.Constant(valueToSearch), toStringMethod))
                    : n.Type == typeof(DateTime) || n.Type == typeof(DateTime?)
                        ? Expression.Call(Expression.Call(n, toStringMethod), containsMethod, Expression.Call(Expression.Constant(dateToSearch), toStringMethod))
                        : Expression.Call(Expression.Call(n, toStringMethod), containsMethod, Expression.Call(Expression.Constant(valueToSearch), toStringMethod))
                    )
                );

            var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
            return lambda;
        }

        private static string ConvertDateToSQLFormat(string valueToSearch)
        {
            var splittedDate = valueToSearch.Split("/");
            var dateToSearch = $@"{(splittedDate.Length > 2
                                ? $"{splittedDate.ElementAtOrDefault(2)}-{GetFilledDateSegment(splittedDate.ElementAtOrDefault(0))}-{GetFilledDateSegment(splittedDate.ElementAtOrDefault(1))}"
                                : splittedDate.Length > 1
                                ? $@"{(splittedDate.ElementAtOrDefault(1).Length > 2
                                    ? $"{splittedDate.ElementAtOrDefault(1)}-{GetFilledDateSegment(splittedDate.ElementAtOrDefault(0))}" : $"{GetFilledDateSegment(splittedDate.ElementAtOrDefault(0))}-{GetFilledDateSegment(splittedDate.ElementAtOrDefault(1))}")}"
                                    : splittedDate.ElementAtOrDefault(0))}";
            return dateToSearch;
        }
        private static string GetFilledDateSegment(string dateSegment)
        {
            return (dateSegment.Length == 1 ? $"0{(dateSegment)}" : dateSegment);
        }
        public static IEnumerable<object> ApplyFilters(this IEnumerable<object> source, Dictionary<string, string> filters)
        {
            List<object> filteredCollection = new List<object>();
            foreach (var entity in source)
            {
                bool filterMatches = true;
                if (filters != null)
                    foreach (var filter in filters)
                    {
                        var currentPropertyInfo = entity.GetType().GetProperty(filter.Key);
                        if (currentPropertyInfo != null)
                            filterMatches = currentPropertyInfo.GetValue(entity).ToString().Equals(filter.Value, StringComparison.OrdinalIgnoreCase);
                        else
                        {
                            filterMatches = false;
                            break;
                        }
                    }
                if (filterMatches)
                    filteredCollection.Add(entity);
            }
            return filteredCollection;
        }
    }
}
