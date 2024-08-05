using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Localization;

namespace ResourcesLibrary
{
    public interface ISharedResource
    {
    }
    public class SharedResource : ISharedResource
    {
        private readonly IStringLocalizer<SharedResource> _localizer;

        public SharedResource(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        public string this[string index]
        {
            get
            {
                return _localizer[index];
            }
        }
    }

    public static class CustomResource
    {
        public static string GetResxNameBykey(string key, string nameSpaceResource)
        {
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager(nameSpaceResource, Assembly.GetExecutingAssembly());


            var entry =
                rm.GetResourceSet(System.Threading.Thread.CurrentThread.CurrentCulture, true, true)
                  .OfType<System.Collections.DictionaryEntry>()
                  .FirstOrDefault(e => e.Key.ToString() == key);

            var value = entry.Value.ToString();
            return value;

        }
    }
}
