using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Utils.I18n
{
    public class Localizer : IStringLocalizer
    {

        private readonly IMemoryCache _cache;
        private readonly JsonSerializer _serializer = new();

        public Localizer(IMemoryCache cache)
        {
            _cache = cache;
        }

        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(name, value ?? name, value == null);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var actualValue = this[name];
                return !actualValue.ResourceNotFound
                    ? new LocalizedString(name, string.Format(actualValue.Value, arguments), false)
                    : actualValue;
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var filePath = $"I18n/{Thread.CurrentThread.CurrentCulture.Name}.json";
            using var str = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var sReader = new StreamReader(str);
            using var reader = new JsonTextReader(sReader);
            while (reader.Read())
            {
                if (reader.TokenType != JsonToken.PropertyName)
                {
                    continue;
                }

                var key = (string)reader.Value ?? string.Empty;
                reader.Read();
                var value = _serializer.Deserialize<string>(reader) ?? string.Empty;
                yield return new LocalizedString(key, value, false);
            }
        }

        private string GetString(string key)
        {
            var relativeFilePath = $"I18n/{Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName}.json";
            var fullFilePath = Path.GetFullPath(relativeFilePath);
            if (!File.Exists(fullFilePath))
            {
                return default;
            }

            var cacheKey = $"locale_{Thread.CurrentThread.CurrentCulture.Name}_{key}";
            var cacheValue = _cache.Get<string>(cacheKey);
            if (!string.IsNullOrEmpty(cacheValue))
            {
                return cacheValue;
            }

            var result = GetValueFromJson(key, Path.GetFullPath(relativeFilePath));
            if (!string.IsNullOrEmpty(result))
            {
                _cache.Set(cacheKey, result);
            }

            return result;
        }

        private string GetValueFromJson(string propertyName, string filePath)
        {
            if (propertyName == null)
            {
                return default;
            }

            if (filePath == null)
            {
                return default;
            }

            string json;
            using (var r = new StreamReader(filePath))
            {
                json = r.ReadToEnd();
            }

            var rss = JObject.Parse(json);

            var nodes = propertyName.Split(":");

            var result = nodes.Length switch
            {
                1 => (string)rss[nodes[0]],
                2 => (string)rss[nodes[0]]?[nodes[1]],
                _ => string.Empty
            };

            return result;
        }
    }
}
