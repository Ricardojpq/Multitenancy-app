using System.Globalization;
using System.Net;
using System.Text;
using System.Text.Json;
using Utils.BaseController;
using Utils.I18n;
using Microsoft.AspNetCore.Http;

namespace Utils.Middlewares;

public class LocalizationMiddleware : IMiddleware
    {

        private readonly static List<string> Locales = new()
        {
            LocaleConstants.En,
            LocaleConstants.Es
        };

        private const string _responseOperationName = "LocaleVerification/Middleware";
        private const string _responseContentType = "application/json; charset=UTF-8";
        private readonly static IResponseUtil ResponseUtil = new ResponseUtil();
        private const string _defaultLocale = LocaleConstants.En;
        private const string _esLocale = "es-VE";
        private const string _enLocale = "en-US";
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                var cultureKey = context.Request.Headers.ContainsKey("Locale")
                    ? context.Request.Headers["Locale"].ToString().ToLower()
                    : _defaultLocale;

                if (!ValidateCulture(cultureKey))
                {
                    await SendForbiddenResponse(context, cultureKey);
                    return;
                }

                var locale = GetLocale(cultureKey);
                var culture = new CultureInfo(locale);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;

                await next(context);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await next(context);
            }
        }

        private string GetLocale(string cultureKey)
        {
            return cultureKey switch
            {
                LocaleConstants.Es => _esLocale,
                LocaleConstants.En => _enLocale,
                _ => _enLocale
            };
        }

        private static bool ValidateCulture(string cultureName)
        {
            return Locales.Any(locale => string.Equals(locale, cultureName ?? string.Empty, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        ///     Send a 403 response status code to the response.
        /// </summary>
        /// <param name="context">HttpContext object</param>
        /// <param name="locale"></param>
        /// <returns></returns>
        private async static Task SendForbiddenResponse(HttpContext context, string locale)
        {
            var localeMsg = string.IsNullOrEmpty(locale) ? "Empty locale" : locale;
            var response403 = new Response403(Guid.NewGuid(), _responseOperationName, $"The Locale ({localeMsg}) is not Supported");
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(JsonSerializer.Serialize(response403));
            context.Response.ContentType = _responseContentType;
            await ResponseUtil.WriteAsync(context, stringBuilder);
        }
    }