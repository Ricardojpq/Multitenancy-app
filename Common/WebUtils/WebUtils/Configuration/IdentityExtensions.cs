#nullable enable
using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebUtils.Configuration
{
    public static class IdentityExtensions
    {
        private static DateTime? _cacheMark;
        private static DiscoveryDocumentResponse? _discoveryDocument;

        public static async Task ConfigureClient(
            this HttpClient client, AccessTokenRequest options, int cacheDurationHours = 8)
        {
            client.BaseAddress = new Uri(options.BaseAddress);

            var elapsedTime = DateTime.Now - _cacheMark.GetValueOrDefault();
            if (_discoveryDocument is null || elapsedTime >= TimeSpan.FromHours(cacheDurationHours))
            {
                _discoveryDocument = await client.GetDiscoveryDocumentAsync(
                    new DiscoveryDocumentRequest
                    {
                        Address = options.IdentityUrl,
                        Policy = { RequireHttps = !options.EnvIsDevelopment }
                    });

                if (_discoveryDocument.IsError)
                {
                    throw new ApplicationException(
                        $"Couldn't get discovery document: {_discoveryDocument.HttpResponse.RequestMessage}");
                }

                _cacheMark = DateTime.Now;
            }

            var token = await client.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = _discoveryDocument.TokenEndpoint,
                    ClientId = options.ClientId,
                    ClientSecret = options.ClientSecret
                });

            if (token.IsError)
            {
                throw new ApplicationException(
                    $"Couldn't get access token: {token.HttpResponse.RequestMessage}");
            }

            client.SetBearerToken(token.AccessToken);
        }
    }

    public struct AccessTokenRequest
    {
        public string BaseAddress { get; set; }
        public string IdentityUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public bool EnvIsDevelopment { get; set; }
    }
}
