using Utils.Shared;
using System.Reflection;

namespace Utils.Extensions
{
    public static class DataTableExtensions
    {

        public static DTSource<T> ToDataTableSource<T>(this IEnumerable<T> collection, DTParams dTParams, IEnumerable<string> propertiesSearch = null, Type entityType = null, bool singleEntity = false)
        {
            var type = entityType ?? typeof(T);

            IEnumerable<T> filteredCollection = collection;

            if (singleEntity)
                filteredCollection = string.IsNullOrEmpty(dTParams.Search["value"]) ? collection : collection.Search(dTParams.Search["value"], propertiesSearch, type);

            var dataTableSource = new DTSource<T>
            {
                Draw = dTParams.Draw,
                RecordsTotal = filteredCollection.Count(),
                RecordsFiltered = filteredCollection.Count(),
                Data = filteredCollection.Skip(dTParams.Start).Take(dTParams.Length).ToList()
                // Data = filteredCollection.Search(dTParams.Search["value"],entityType: type).Skip(dTParams.Start).Take(dTParams.Length).ToList()
            };
            return dataTableSource;
        }

        public static DTSource<T> ToLookupDataTableSource<T>(this IEnumerable<T> collection, DTParams dTParams)
        {
            // var searchValue = dTParams.Search != null ? dTParams.Search["value"] : string.Empty;
            //var filteredCollection = string.IsNullOrEmpty(dTParams.Search["value"]) ? collection : collection.Search(dTParams.Search["value"]);

            var filteredCollection = string.IsNullOrEmpty(dTParams.Search["value"]) ? collection : collection.TryFilterJObjectToDataTable(dTParams.Search["value"]);
            foreach (var order in dTParams.Order)
            {
                string columnName = "";
                bool isDesc = order.Dir == "desc" ? true : false;
                filteredCollection.OrderByDynamic(columnName, isDesc);
            }
            var dataTableSource = new DTSource<T>
            {
                Draw = dTParams.Draw,
                RecordsTotal = filteredCollection.Count(),
                RecordsFiltered = filteredCollection.Count(),
                Data = (filteredCollection as IEnumerable<T>).Skip(dTParams.Start).Take(dTParams.Length).ToList()
            };

            return dataTableSource;
        }
        public static IEnumerable<T> Search<T>(this IEnumerable<T> collection, List<string> fields, List<string> values)
        {
            if (collection == null) return collection;
            if (collection.Count() <= 0) return collection;

            Dictionary<string, string> dc = fields.Select((v, i) => new { i, v }).ToDictionary(x => x.v, x => values[x.i]);
            Dictionary<string, PropertyInfo> dcPropertyInfo = new Dictionary<string, PropertyInfo>();
            object firstDefaultElement = collection.FirstOrDefault();
            fields.ForEach(x => dcPropertyInfo[x] = firstDefaultElement.getPropertyInfo(x));
            Func<T, Dictionary<string, PropertyInfo>, KeyValuePair<string, string>, bool> func = delegate (T x, Dictionary<string, PropertyInfo> dcProperty, KeyValuePair<string, string> field)
            {
                try
                {
                    string val = x.GetStringValueFromObject(dcProperty[field.Key]);
                    return val.IndexOf(field.Value, StringComparison.CurrentCultureIgnoreCase) >= 0;
                }
                catch
                {
                    return false;
                }
            };

            collection = collection.Where(x => dc.FirstOrDefault(field => func(x, dcPropertyInfo, field)).Key != null);
            return collection;
        }
        public static IQueryable<T> Search<T>(this IQueryable<T> collection, string searchWord)
        {
            var type = typeof(T);

            if (type.HasProperty(Constants.SearchableFields))
            {
                T first = collection.FirstOrDefault();
                var searchableProperties = first != null ? first.GetValueFromObject<IEnumerable<string>>(Constants.SearchableFields) : new List<string>();
                var properties = type.GetProperties().Where(x => searchableProperties.Contains(x.Name)).ToList();
                var result = string.IsNullOrEmpty(searchWord)
                    ? collection
                    : collection.Where(e => properties.Select(p => p.GetValue(e, null)).Where(p => p != null)
                                .Any(p => p.ToString().IndexOf(searchWord, StringComparison.CurrentCultureIgnoreCase) >= 0));
                return result;
            }
            else
            {
                var properties = type.GetProperties()
                    .Where(x => x.PropertyType == typeof(string) ||
                                x.PropertyType == typeof(int) ||
                                x.PropertyType == typeof(decimal) ||
                                x.PropertyType == typeof(double) ||
                                x.PropertyType == typeof(DateTime))
                    .ToList();


                var result = string.IsNullOrEmpty(searchWord)
                    ? collection
                    : collection.Where(
                        e => properties
                        .Select(p => p.GetValue(e, null))
                        .Where(p => p != null)
                        .Any(p => p.ToString().IndexOf(searchWord, StringComparison.CurrentCultureIgnoreCase) >= 0));

                return result;
            }
        }
        public static IQueryable<T> SearchCore3_1<T>(this IQueryable<T> collection, string searchWord)
        {
            var type = typeof(T);

            if (type.HasProperty(Constants.SearchableFields))
            {
                T first = collection.FirstOrDefault();
                var searchableProperties = first != null ? first.GetValueFromObject<IEnumerable<string>>(Constants.SearchableFields) : new List<string>();
                var properties = type.GetProperties().Where(x => searchableProperties.Contains(x.Name)).ToList();
                var result = string.IsNullOrEmpty(searchWord)
                    ? collection
                    : collection.AsEnumerable().Where(e => properties.Select(p => p.GetValue(e, null)).Where(p => p != null)
                                .Any(p => p.ToString().IndexOf(searchWord, StringComparison.CurrentCultureIgnoreCase) >= 0)).AsQueryable();
                return result;
            }
            else
            {
                var properties = type.GetProperties()
                    .Where(x => x.PropertyType == typeof(string) ||
                                x.PropertyType == typeof(int) ||
                                x.PropertyType == typeof(decimal) ||
                                x.PropertyType == typeof(double) ||
                                x.PropertyType == typeof(DateTime))
                    .ToList();


                var result = string.IsNullOrEmpty(searchWord)
                    ? collection
                    : collection.AsEnumerable().Where(
                        e => properties
                        .Select(p => p.GetValue(e, null))
                        .Where(p => p != null)
                        .Any(p => p.ToString().IndexOf(searchWord, StringComparison.CurrentCultureIgnoreCase) >= 0)).AsQueryable();

                return result;
            }
        }
        public static IQueryable<T> Search<T>(this IQueryable<T> collection, List<string> fields, List<string> values)
        {
            if (collection == null) return collection;
            if (collection.Count() <= 0) return collection;

            for (int i = 0; i < values.Count; i++)
            {
                if (DateTime.TryParse(values[i], out DateTime result))
                {
                    DateTime aux = DateTime.Parse(values[i]);
                    values[i] = $"{aux.Month.ToString().TrimStart('0')}/{aux.Day.ToString().TrimStart('0')}/{aux.Year}";
                }
            }

            Dictionary<string, string> dc = fields.Select((v, i) => new { i, v }).ToDictionary(x => x.v, x => values[x.i]);
            Dictionary<string, PropertyInfo> dcPropertyInfo = new Dictionary<string, PropertyInfo>();
            object firstDefaultElement = collection.FirstOrDefault();
            fields.ForEach(x => dcPropertyInfo[x] = firstDefaultElement.getPropertyInfo(x));
            Func<T, Dictionary<string, PropertyInfo>, Dictionary<string, string>, bool> func = delegate (T x, Dictionary<string, PropertyInfo> dcProperty, Dictionary<string, string> dcFields)
            {
                try
                {
                    bool match = false;
                    foreach (KeyValuePair<string, string> dcField in dcFields)
                    {
                        string val = x.GetStringValueFromObject(dcProperty[dcField.Key]);
                        match = val.IndexOf(dcField.Value.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0;
                        if (match)
                            break;
                    }
                    return match;
                }
                catch
                {
                    return false;
                }
            };
            collection = collection.Where(x => func(x, dcPropertyInfo, dc));
            return collection;
        }
        public static IQueryable<T> SearchCore3_1<T>(this IQueryable<T> collection, List<string> fields, List<string> values)
        {
            if (collection == null) return collection;
            if (collection.Count() <= 0) return collection;

            for (int i = 0; i < values.Count; i++)
            {
                if (DateTime.TryParse(values[i], out DateTime result))
                {
                    DateTime aux = DateTime.Parse(values[i]);
                    values[i] = $"{aux.Month.ToString().TrimStart('0')}/{aux.Day.ToString().TrimStart('0')}/{aux.Year}";
                }
            }

            Dictionary<string, string> dc = fields.Select((v, i) => new { i, v }).ToDictionary(x => x.v, x => values[x.i]);
            Dictionary<string, PropertyInfo> dcPropertyInfo = new Dictionary<string, PropertyInfo>();
            object firstDefaultElement = collection.FirstOrDefault();
            fields.ForEach(x => dcPropertyInfo[x] = firstDefaultElement.getPropertyInfo(x));
            Func<T, Dictionary<string, PropertyInfo>, Dictionary<string, string>, bool> func = delegate (T x, Dictionary<string, PropertyInfo> dcProperty, Dictionary<string, string> dcFields)
            {
                try
                {
                    bool match = false;
                    foreach (KeyValuePair<string, string> dcField in dcFields)
                    {
                        string val = x.GetStringValueFromObject(dcProperty[dcField.Key]);
                        match = val.IndexOf(dcField.Value.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0;
                        if (match)
                            break;
                    }
                    return match;
                }
                catch
                {
                    return false;
                }
            };
            collection = collection.AsEnumerable().Where(x => func(x, dcPropertyInfo, dc)).AsQueryable();
            return collection;
        }
        public static IEnumerable<T> Search<T>(this IEnumerable<T> collection, string property, string value)
        {
            if (collection == null) return collection;
            if (collection.Count() <= 0) return collection;
            Func<T, string, string, bool> func = delegate (T x, string prop, string val)
            {
                try
                {
                    string propertyValue = x.GetStringValueFromObject(property);
                    return propertyValue.IndexOf(value, StringComparison.CurrentCultureIgnoreCase) >= 0;
                }
                catch { return false; }
            };
            collection = collection.Where(x => func(x, property, value));
            return collection;
        }
        public static IEnumerable<T> Search<T>(this IEnumerable<T> collection, string searchWord, IEnumerable<string> propertiesSearch = null, Type entityType = null)
        {
            var type = entityType ?? typeof(T);

            if (propertiesSearch != null && propertiesSearch.Any())
            {

                var properties = type.GetProperties().Where(x => propertiesSearch.Contains(x.Name)).ToList();
                var result = string.IsNullOrEmpty(searchWord)
                    ? collection
                    : collection.Where(e => properties.Select(p => p.GetValue(e, null)).Where(p => p != null)
                                .Any(p => p.ToString().IndexOf(searchWord, StringComparison.CurrentCultureIgnoreCase) >= 0));
                return result;
            }
            else if (type.HasProperty(Constants.SearchableFields))
            {
                T first = collection.FirstOrDefault();
                var searchableProperties = first != null ? first.GetValueFromObject<IEnumerable<string>>(Constants.SearchableFields) : new List<string>();
                var properties = type.GetProperties().Where(x => searchableProperties.Contains(x.Name)).ToList();
                var result = string.IsNullOrEmpty(searchWord)
                    ? collection
                    : collection.Where(e => properties.Select(p => p.GetValue(e, null)).Where(p => p != null)
                                .Any(p => p.ToString().IndexOf(searchWord, StringComparison.CurrentCultureIgnoreCase) >= 0));
                return result;
            }
            else
            {
                var properties = type.GetProperties()
                    .Where(x => x.PropertyType == typeof(string) ||
                                x.PropertyType == typeof(int) ||
                                x.PropertyType == typeof(decimal) ||
                                x.PropertyType == typeof(double) ||
                                x.PropertyType == typeof(DateTime))
                    .ToList();
                var result = string.IsNullOrEmpty(searchWord)
                    ? collection
                    : collection.Where(e => properties.Select(p => p.GetValue(e, null)).Where(p => p != null)
                                .Any(p => p.ToString().IndexOf(searchWord, StringComparison.CurrentCultureIgnoreCase) >= 0));
                return result;
            }
        }
        public static IQueryable<T> SearchExact<T>(this IQueryable<T> collection, List<string> fields, List<string> values)
        {
            if (collection == null) return collection;
            if (collection.Count() <= 0) return collection;
            Dictionary<string, string> dc = fields.Select((v, i) => new { i, v }).ToDictionary(x => x.v, x => values[x.i]);
            Dictionary<string, PropertyInfo> dcPropertyInfo = new Dictionary<string, PropertyInfo>();
            object firstDefaultElement = collection.FirstOrDefault();
            fields.ForEach(x => dcPropertyInfo[x] = firstDefaultElement.getPropertyInfo(x));
            Func<T, Dictionary<string, PropertyInfo>, KeyValuePair<string, string>, bool> func = delegate (T x, Dictionary<string, PropertyInfo> dcProperty, KeyValuePair<string, string> field)
            {
                try
                {
                    string val = x.GetStringValueFromObject(dcProperty[field.Key]);
                    return val.Equals(field.Value, StringComparison.CurrentCultureIgnoreCase);
                }
                catch
                {
                    return false;
                }
            };
            collection = collection.Where(x => dc.FirstOrDefault(field => func(x, dcPropertyInfo, field)).Key != null);
            return collection;
        }
        public static IQueryable<T> SearchExactCore3_1<T>(this IQueryable<T> collection, List<string> fields, List<string> values)
        {
            if (collection == null) return collection;
            if (collection.Count() <= 0) return collection;
            Dictionary<string, string> dc = fields.Select((v, i) => new { i, v }).ToDictionary(x => x.v, x => values[x.i]);
            Dictionary<string, PropertyInfo> dcPropertyInfo = new Dictionary<string, PropertyInfo>();
            object firstDefaultElement = collection.FirstOrDefault();
            fields.ForEach(x => dcPropertyInfo[x] = firstDefaultElement.getPropertyInfo(x));
            Func<T, Dictionary<string, PropertyInfo>, KeyValuePair<string, string>, bool> func = delegate (T x, Dictionary<string, PropertyInfo> dcProperty, KeyValuePair<string, string> field)
            {
                try
                {
                    string val = x.GetStringValueFromObject(dcProperty[field.Key]);
                    return val.Equals(field.Value, StringComparison.CurrentCultureIgnoreCase);
                }
                catch
                {
                    return false;
                }
            };
            collection = collection.AsEnumerable().Where(x => dc.FirstOrDefault(field => func(x, dcPropertyInfo, field)).Key != null).AsQueryable();
            return collection;
        }
        public static IEnumerable<T> SearchExact<T>(this IEnumerable<T> collection, List<string> fields, List<string> values)
        {
            if (collection == null) return collection;
            if (collection.Count() <= 0) return collection;
            Dictionary<string, string> dc = fields.Select((v, i) => new { i, v }).ToDictionary(x => x.v, x => values[x.i]);
            Dictionary<string, PropertyInfo> dcPropertyInfo = new Dictionary<string, PropertyInfo>();
            object firstDefaultElement = collection.FirstOrDefault();
            fields.ForEach(x => dcPropertyInfo[x] = firstDefaultElement.getPropertyInfo(x));
            Func<T, Dictionary<string, PropertyInfo>, KeyValuePair<string, string>, bool> func = delegate (T x, Dictionary<string, PropertyInfo> dcProperty, KeyValuePair<string, string> field)
            {
                try
                {
                    string val = x.GetStringValueFromObject(dcProperty[field.Key]);
                    return val.Equals(field.Value, StringComparison.CurrentCultureIgnoreCase);
                }
                catch
                {
                    return false;
                }
            };
            collection = collection.Where(x => dc.FirstOrDefault(field => func(x, dcPropertyInfo, field)).Key != null);
            return collection;
        }
        public static bool Include(this string s, string substr) => (s ?? "").IndexOf(substr, StringComparison.CurrentCultureIgnoreCase) >= 0;

        public static bool AOrBInclude(this object x, Dictionary<string, string> dcFields, Dictionary<string, PropertyObject> dcPropertyObject)
        {
            bool fi = false;
            foreach (var item in dcFields)
            {
                fi |= x.GetStringValueFromObject(item.Key, dcPropertyObject[item.Key]).Include(item.Value);
            }
            return fi;
        }

        public static bool AAndBInclude(this object x, Dictionary<string, string> dcFields, Dictionary<string, PropertyObject> dcPropertyObject)
        {
            foreach (var item in dcFields)
            {
                if (!x.GetStringValueFromObject(item.Key, dcPropertyObject[item.Key]).Include(item.Value)) return false;
            }
            return true;
        }

        public static bool AAndBEquals(this object x, Dictionary<string, string> dcFields, Dictionary<string, PropertyObject> dcPropertyObject)
        {
            foreach (var item in dcFields)
            {
                if (!x.GetStringValueFromObject(item.Key, dcPropertyObject[item.Key]).Equals(item.Value)) return false;
            }
            return true;
        }
        public static bool DateAAndBBetween(this object x, Dictionary<string, PropertyObject> dcPropertyObject, DateTime? effectiveAsOf)
        {
            if (effectiveAsOf == null)
                return true;
            DateTime effectiveDate;
            DateTime expirationDate;

            DateTime.TryParse(x.GetStringValueFromObject(Constants.PropertyEffectiveDate, dcPropertyObject[Constants.PropertyEffectiveDate]), out effectiveDate);
            DateTime.TryParse(x.GetStringValueFromObject(Constants.PropertyExpirationDate, dcPropertyObject[Constants.PropertyExpirationDate]), out expirationDate);

            if (!((effectiveDate.Date <= effectiveAsOf?.Date)
                && (expirationDate.Date >= effectiveAsOf?.Date
                || expirationDate.Date == DateTime.MinValue.Date)))
                return false;

            return true;
        }

        public static bool DateAAndBBetween(this object x, DateTime? effectiveAsOf)
        {
            if (effectiveAsOf == null)
                return true;

            DateTime effectiveDate;
            DateTime expirationDate;

            DateTime.TryParse(x.GetStringValueFromObject(Constants.PropertyEffectiveDate), out effectiveDate);
            DateTime.TryParse(x.GetStringValueFromObject(Constants.PropertyExpirationDate), out expirationDate);

            if (!((effectiveDate.Date <= effectiveAsOf?.Date)
                && (expirationDate.Date >= effectiveAsOf?.Date
                || expirationDate.Date == DateTime.MinValue.Date)))
                return false;

            return true;
        }

        public static bool DateAAndBBetweenCustom(this object x, DateTime? dateFilterValue, string dateFilterField)
        {
            if (dateFilterValue == null)
                return true;

            DateTime dateValueEntity;
            DateTime.TryParse(x.GetStringValueFromObject(dateFilterField), out dateValueEntity);
            

            if (!(dateValueEntity.Date <= dateFilterValue?.Date))
                return false;

            return true;
        }

        public static bool DateAAndBEqualCustom(this object x, DateTime? dateFilterValue, string dateFilterField)
        {
            if (dateFilterValue == null)
                return true;

            DateTime dateValueEntity;
            DateTime.TryParse(x.GetStringValueFromObject(dateFilterField), out dateValueEntity);


            if (!(dateValueEntity.Date == dateFilterValue?.Date))
                return false;

            return true;
        }

        public static bool AORBMultipleInclude<T>(this T element, List<string> _fields, List<string> _values, List<string> _types)
        {
            if (element == null) return false;
            Func<T, List<string>, List<string>, List<string>, bool> funcMultipleFilter = delegate (T obj, List<string> fields, List<string> values, List<string> types)
            {
                var tp = typeof(T);
                int len = fields.Count;
                for (int i = 0; i < len; i++)
                {
                    string f = fields[i], v = values[i] ?? "", t = types[i];
                    if (t.Equals(Constants.DATETIMEON))
                    {
                        v = v.Substring(1, v.Length - 2);
                        var dates = v.Split(';');
                        string from = dates[0], to = dates[1];
                        DateTime currentDate = obj.GetValueFromObject<DateTime>(f);
                        bool left = !string.IsNullOrEmpty(from) ? DateTime.Parse(from) <= currentDate : true;
                        bool rigth = !string.IsNullOrEmpty(to) ? DateTime.Parse(to) >= currentDate : true;
                        if (!(left & rigth)) return false;
                    }
                    else if (t.Equals(Constants.DATETIMEIN))
                    {
                        DateTime currentDate = obj.GetValueFromObject<DateTime>(f);
                        if (DateTime.Parse(v) != currentDate)
                            return false;
                    }
                    else
                    {
                        string valueObject = obj.GetStringValueFromObject(f) ?? "";
                        if (valueObject.IndexOf(v, StringComparison.CurrentCultureIgnoreCase) < 0) return false;
                    }
                }
                return true;
            };
            var result = funcMultipleFilter(element, _fields, _values, _types);
            return result;
        }
        //TODO - Delete?
        #region Delete
        //public static DTSource<T> ToDataTableSource<T>(this IEnumerable<T> collection, DTParams dTParams, IEnumerable<string> propertiesSearch = null)
        //{
        //    // var searchValue = dTParams.Search != null ? dTParams.Search["value"] : string.Empty;
        //    //var filteredCollection = string.IsNullOrEmpty(dTParams.Search["value"]) ? collection : collection.Search(dTParams.Search["value"]);

        //    var filteredCollection = string.IsNullOrEmpty(dTParams.Search["value"]) ? collection : collection.Search(dTParams.Search["value"], propertiesSearch);

        //    var dataTableSource = new DTSource<T>
        //    {
        //        Draw = dTParams.Draw,
        //        RecordsTotal = filteredCollection.Count(),
        //        RecordsFiltered = filteredCollection.Count(),
        //        Data = filteredCollection.Skip(dTParams.Start).Take(dTParams.Length).ToList()
        //    };

        //    return dataTableSource;
        //}

        //public static IEnumerable<T> Search<T>(this IEnumerable<T> collection, string searchWord, string searchProperties)
        //{
        //    IEnumerable<string> listProperties = searchProperties.Split(',');
        //    var properties = collection.FirstOrDefault().GetType().GetProperties().
        //        Where(x => (x.Name != "IsActive" &&
        //        x.Name != "CreatedDate" &&
        //        x.Name != "MaintenanceUser") &&
        //        listProperties.Any(y => y == x.Name)).ToList();

        //    if (properties != null && !string.IsNullOrEmpty(searchWord))
        //    {
        //        var result = properties.First() == properties.Last() ?
        //            collection.Where(x => (properties.First().GetValue(x, null) ?? "").ToString().IndexOf(searchWord, StringComparison.CurrentCultureIgnoreCase) >= 0) :
        //            collection.Where(x => properties.Any(y => (y.GetValue(x, null) ?? "").ToString().IndexOf(searchWord, StringComparison.CurrentCultureIgnoreCase) >= 0));
        //        return result;
        //    }
        //    else
        //        return collection;
        //}

        //public static DTSource<T> ToDataTableSource<T>(this IQueryable<T> collection, DTParams dTParams) where T : class
        //{
        //    // var searchValue = dTParams.Search != null ? dTParams.Search["value"] : string.Empty;
        //    var filteredCollection = string.IsNullOrEmpty(dTParams.Search["value"]) ? collection : (collection as IQueryable<T>).Search(dTParams.Search["value"]);
        //    System.Diagnostics.Debug.WriteLine($"Draw: {dTParams.Draw} | SearchValue: {dTParams.Search}");

        //    var dataTableSource = new DTSource<T>
        //    {
        //        Draw = dTParams.Draw,
        //        RecordsTotal = filteredCollection.Count(),
        //        RecordsFiltered = filteredCollection.Count(),
        //        Data = filteredCollection.Skip(dTParams.Start).Take(dTParams.Length).ToList()
        //    };

        //    return dataTableSource;
        //}
        //public static IEnumerable<T> Search<T>(this IEnumerable<T> collection, string searchWord, string searchProperty, Type type)
        //{
        //    var property = type.GetProperties()
        //            .Where(x => x.Name == searchProperty)
        //            .FirstOrDefault();

        //    if (property != null)
        //    {
        //        var result = string.IsNullOrEmpty(searchWord)
        //            ? collection
        //            : collection.Where(x => property.GetValue(x, null).ToString().ToLower().Contains(searchWord.ToLower()));
        //        return result;
        //    }
        //    else
        //        return collection;
        //}

        //public static IEnumerable<T> Search<T>(this IEnumerable<T> collection, string searchWord, string searchProperty)
        //{
        //    var type = collection.GetType().GetGenericArguments().Single();

        //    var property = type.GetProperties()
        //            .Where(x => x.Name == searchProperty)
        //            .FirstOrDefault();

        //    if (property != null)
        //    {
        //        var result = string.IsNullOrEmpty(searchWord)
        //            ? collection
        //            : collection.Where(x => property.GetValue(x, null).ToString().ToLower().Contains(searchWord.ToLower()));
        //        return result;
        //    }
        //    else
        //        return collection;
        //}
        #endregion Delete
    }
}
