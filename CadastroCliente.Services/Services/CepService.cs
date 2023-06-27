using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CadastroCliente.Model;

namespace CadastroCliente.Services.Services
{
    public class CepService : ICepService
    {
        private readonly IApiConfigService _apiConfigService;
        private readonly HttpClient _httpClient;

        public CepService(IApiConfigService apiConfigService, HttpClient httpClient)
        {
            _apiConfigService = apiConfigService;
            _httpClient = httpClient;
        }

        public async Task<Cep> GetCep(string cep)
        {
            //https://viacep.com.br/ws/{cep}/json/

            string cepUrl = _apiConfigService.GetCepUrl();
            cepUrl = cepUrl.Replace("{cep}", cep);

            var response = await _httpClient.GetAsync(cepUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Cep>(content);
            }

            return null;
        }
    }
}
