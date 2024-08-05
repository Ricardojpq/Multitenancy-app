using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;

namespace Utils.I18n
{
    public class LocalizerFactory : IStringLocalizerFactory
    {
        private readonly IMemoryCache _cache;

        public LocalizerFactory(IMemoryCache cache)
        {
            _cache = cache;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return StringLocalizer();
        }


        public IStringLocalizer Create(string baseName, string location)
        {
            return StringLocalizer();
        }

        private IStringLocalizer StringLocalizer()
        {
            return new Localizer(_cache);
        }
    }
}
