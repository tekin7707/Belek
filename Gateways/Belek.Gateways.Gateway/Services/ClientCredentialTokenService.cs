using Belek.Gateways.Gateway.Services;
using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;

namespace Belek.Gateways.Gateway
{
    public class ClientCredentialTokenService : IClientCredentialTokenService
    {

        private readonly IClientAccessTokenCache _clientAccessTokenCache;
        private readonly HttpClient _httpClient;

        public ClientCredentialTokenService(IClientAccessTokenCache clientAccessTokenCache, HttpClient httpClient)
        {
            _clientAccessTokenCache = clientAccessTokenCache;
            _httpClient = httpClient;
        }

        public async Task<string> GetToken()
        {
            var currentToken = await _clientAccessTokenCache.GetAsync("WebClientToken");

            if (currentToken != null)
            {
                return currentToken.AccessToken;
            }

            var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = "https://localhost:5001",
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (disco.IsError)
            {
                throw disco.Exception;
            }

            var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = "WebMvcClient",
                ClientSecret = "secret",
                Address = disco.TokenEndpoint
            };

            var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);

            if (newToken.IsError)
            {
                throw newToken.Exception;
            }

            await _clientAccessTokenCache.SetAsync("WebClientToken", newToken.AccessToken, newToken.ExpiresIn);

            return newToken.AccessToken;
        }
    }
}