using Belek.Gateways.Gateway.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Belek.Gateways.Gateway.Handler
{
    public class ClientCredentialTokenHandler : DelegatingHandler
    {
        private readonly HttpClient _httpClient;

        public ClientCredentialTokenHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "");// await _clientCredentialTokenService.GetToken());

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnAuthorizeException();
            }

            return response;
        }
    }
}