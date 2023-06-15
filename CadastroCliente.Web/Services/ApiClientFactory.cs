using System.Security.Claims;

namespace CadastroCliente.Web.Services
{
    public class ApiClientFactory : IApiClientFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiClientFactory(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public ApiClient Create(string jwtToken)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            return new ApiClient(httpClient, jwtToken);
        }
    }
}
