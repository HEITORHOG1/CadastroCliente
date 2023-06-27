using System.Security.Claims;

namespace CadastroCliente.Web.Services
{
    public class ApiClientFactory : IApiClientFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IServiceProvider _serviceProvider;

        public ApiClientFactory(IHttpClientFactory httpClientFactory, IServiceProvider serviceProvider)
        {
            _httpClientFactory = httpClientFactory;
            _serviceProvider = serviceProvider;
        }

        public ApiClient Create(string jwtToken)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiClient");
            return new ApiClient(httpClient, jwtToken);
        }

        public T CreateClient<T>() where T : class
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
