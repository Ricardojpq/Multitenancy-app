using System.Reflection;
using Newtonsoft.Json;

namespace Utils.Extensions
{
    public static class ObjectUtil
    {
        public static T Instance<T>(this T obj) where T : new() => obj == null ? new T() : obj;
        public static Dictionary<TKey, TValue> Instance<TKey, TValue>(this Dictionary<TKey, TValue> dc) => dc == null ? new Dictionary<TKey, TValue>() : dc;
        //public static IEnumerable<T> Instance<T>(this IEnumerable<T> list) where T : new() => list == null ? new List<T>() : list;
        public static IEnumerable<T> Instance<T>(this IEnumerable<T> list) => list == null ? new List<T>() : list;
        public static T SetFromJsonFile<T>(this T obj, string filePath) where T : new()
        {
            using (StreamReader stream = new StreamReader(filePath))
            {
                string json = stream.ReadToEnd();
                obj = JsonConvert.DeserializeObject<T>(json);
            }

            return obj.Instance();
        }
        public static List<T> getListFromObjectInstance<T>(this T instance) where T : new()
        {
            List<T> list = new List<T> { instance };
            list.RemoveAt(0);
            return list;
        }

        public static bool HasProperty(this object instance, string propertyName) => instance.GetType().HasProperty(propertyName);


        public static bool HasProperty(this Type type, string propertyName)
        {
            return type
                .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Any(p => p.Name == propertyName);
        }

        public static void SetValueFromPropertyName(this object x, string propertyName, object value)
        {
            x.GetType().GetProperty(propertyName).SetValue(x, value);
        }

        public static T GetValueFromObject<T>(this object instance, string propertyName, T defaultValue = default(T))
        {
            T value = defaultValue;
            if (instance.GetType().HasProperty(propertyName)) value = (T)(instance.GetType().GetProperty(propertyName).GetValue(instance) ?? defaultValue);
            return value;
        }
        public static string GetStringValueFromObject(this object instance, string propertyName)
        {
            string value = default(string);
            if (instance.GetType().HasProperty(propertyName)) value = (instance.getPropertyInfo(propertyName).GetValue(instance) ?? "").ToString();
            return value;
        }

        public static string GetStringValueFromObject(this object x, string name, PropertyObject propertyObject)
        {
            string value = default(string);
            if (propertyObject.isNested) value = x.GetStringValueNestedFromPropertyInfo(name, propertyObject.propertyInfo);
            else value = x.GetStringValueFromPropertyInfo(propertyObject.propertyInfo);
            return value;
        }

        public static string GetStringValueNestedFromPropertyInfo(this object x, string nameNested, PropertyInfo propertyInfo)
        {
            string val = default(string);
            string[] names = nameNested.Split(".");
            object baseVal = x;
            PropertyInfo basePropertyInfo;
            Type baseType = x.GetType();
            bool entry = false;
            foreach (string item in names)
            {
                try
                {
                    basePropertyInfo = baseType.GetProperty(item);
                    baseVal = basePropertyInfo.GetValue(baseVal);
                    baseType = basePropertyInfo.PropertyType;
                    entry = true;
                }
                catch (Exception)
                {
                    entry = false;
                }
            }
            if (entry) val = baseVal.ToString();
            return val;
        }

        public static string GetStringValueFromPropertyInfo(this object x, PropertyInfo propertyInfo)
        {
            string val = default(string);
            if (propertyInfo != null)
            {
                object sVal = propertyInfo.GetValue(x);
                if (sVal != null) return sVal.ToString();
            }
            return val;
        }

        public static PropertyInfo getPropertyInfo(this object instance, string propertyName)
        {
            PropertyInfo value = default(PropertyInfo);
            if (instance.GetType().HasProperty(propertyName)) value = instance.GetType().GetProperty(propertyName);
            return value;
        }

        public static string GetStringValueFromObject(this object instance, PropertyInfo propertyInfo)
        {
            string value = default(string);
            if (propertyInfo != null) value = propertyInfo.GetValue(instance).ToString();
            return value;
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this List<TKey> listKey, List<TValue> listValue)
        {
            Dictionary<TKey, TValue> dc = new Dictionary<TKey, TValue>();
            for (int i = 0; i < listKey.Count; i++) dc[listKey[i]] = listValue[i];
            return dc;
        }

        public static Dictionary<TKey, TValue> ToExistInDictionary<TKey, TValue>(this List<TKey> listEval, Dictionary<TKey, TValue> dcBase)
        {
            Dictionary<TKey, TValue> dc = new Dictionary<TKey, TValue>();
            foreach (var item in listEval) if (dcBase.ContainsKey(item)) dc[item] = dcBase[item];

            List<string> a = new List<string>(), b = new List<string>();
            // 123
            return dc;
        }
        public static List<TValue> IntersectWithDictionaryGetListValue<TKey, TValue>(this List<TKey> listBase, Dictionary<TKey, TValue> dc)
        {
            List<TValue> list = new List<TValue>();
            foreach (var item in listBase)
            {
                if (dc.ContainsKey(item)) list.Add(dc[item]);
            }
            return list;
        }

        public static PropertyInfo GetPropertyInfoByName(this Type x, string name)
        {
            PropertyInfo propertyInfo = default(PropertyInfo);
            if (name.Contains("."))
            {
                string[] names = name.Split(".");
                Type typeBase = x;
                foreach (string item in names)
                {
                    propertyInfo = typeBase.GetProperty(item);
                    typeBase = propertyInfo.PropertyType;
                }
            }
            else
                propertyInfo = x.GetProperty(name);
            return propertyInfo;
        }

        public static Dictionary<string, PropertyObject> getPropertiesObject(this Type x, List<string> fields)
        {
            Dictionary<string, PropertyObject> dcPropertyObject = new Dictionary<string, PropertyObject>();
            foreach (var item in fields)
                dcPropertyObject[item] = x.GetPropertyObject(item);
            return dcPropertyObject;
        }
        public static PropertyObject GetPropertyObject(this Type x, string name)
        {
            PropertyObject propertyObject = null;
            //if (name.Contains("."))
            //{
            //    string[] names = name.Split(".");
            //    Type baseType = x;
            //    PropertyInfo basePropertyInfo = null;
            //    foreach (string item in names)
            //    {
            //        basePropertyInfo = baseType.GetProperty(name);
            //        baseType = basePropertyInfo.PropertyType;
            //        return
            //    }
            //}
            //else
            propertyObject = new PropertyObject(name.Contains("."), x.GetPropertyInfoByName(name));
            return propertyObject;
        }

        public static List<string> getStringValueFormat<T>(this List<T> ieX, string formatText)
        {
            List<string> names = new List<string>();
            string format = formatText.getFormatAndNames(ref names);
            string[] nameValues = new string[names.Count];
            List<string> result = new List<string>();
            foreach (var item in ieX)
            {
                for (int i = 0; i < names.Count; i++)
                {
                    nameValues[i] = item.GetStringValueFromObject(names[i]);
                }
                result.Add(string.Format(format, nameValues));
            }
            return result;
        }
        public static List<string> ContainItemList(this List<string> listBase, List<string> listSon)
        {
            listSon.Sort();
            listBase.Sort();
            List<string> list = new List<string>();
            int lenBase = listBase.Count, idx = 0;
            foreach (string item in listSon)
            {
                for (int i = idx; i < lenBase; i++)
                {
                    if (listBase[i].StartsWith(item))
                    {
                        list.Add(item);
                        idx = i;
                        i = lenBase;
                    }
                }
            }
            return list;
        }



    }

    public class PropertyObject
    {
        public bool isNested { get; set; }
        public PropertyInfo propertyInfo { get; set; }
        public PropertyObject propertyObject { get; set; }
        public PropertyObject()
        {

        }
        public PropertyObject(bool isNested, PropertyInfo propertyInfo)
        {
            this.isNested = isNested;
            this.propertyInfo = propertyInfo;
        }
    }
}