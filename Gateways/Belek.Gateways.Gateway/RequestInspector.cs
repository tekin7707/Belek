using Belek.Gateways.Gateway.Services;
using System.Net.Http.Headers;

namespace Belek.Gateways.Gateway
{
    public class RequestInspector : DelegatingHandler
    {
        private readonly HttpClient _httpClient;
        private readonly IClientCredentialTokenService _clientCredentialTokenService;

        public RequestInspector(HttpClient httpClient, IClientCredentialTokenService clientCredentialTokenService)
        {
            _httpClient = httpClient;
            _clientCredentialTokenService = clientCredentialTokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"\nDevam etmeden önce şu gelen Request içeriğini bir inceyelim\n{request.ToString()}\n");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _clientCredentialTokenService.GetToken());

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
