using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _jwtToken;

    public ApiClient(HttpClient httpClient, string jwtToken)
    {
        _httpClient = httpClient;
        _jwtToken = jwtToken;

        if (!string.IsNullOrEmpty(_jwtToken))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);
        }
    }
    public async Task<T> GetAsync<T>(string requestUri, string searchTerm)
    {
        var response = await _httpClient.GetAsync($"{requestUri}?termoBusca={searchTerm}");

        var content = await response.Content.ReadAsStringAsync();

        // Imprima o conteúdo da resposta em caso de falha
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("API call failed with response: " + content);
        }
        response.EnsureSuccessStatusCode();

        return JsonConvert.DeserializeObject<T>(content);
    }



    public async Task<T> GetAsync<T>(string requestUri)
    {
        var response = await _httpClient.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(content);
    }

    public async Task<HttpResponseMessage> PostAsync<T>(string requestUri, T content)
    {
        var contentJson = JsonConvert.SerializeObject(content);
        var httpContent = new StringContent(contentJson, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(requestUri, httpContent);

        if (!response.IsSuccessStatusCode)
        {
            // Leia o conteúdo de erro como uma string
            var errorContent = await response.Content.ReadAsStringAsync();

            // Retorne a resposta com o conteúdo de erro
            return new HttpResponseMessage
            {
                StatusCode = response.StatusCode,
                ReasonPhrase = response.ReasonPhrase,
                Content = new StringContent(errorContent, Encoding.UTF8, "application/json")
            };
        }

        // Retorne a resposta bem-sucedida
        return response;
    }

    public async Task<HttpResponseMessage> PutAsync<T>(string requestUri, T content)
    {
        var contentJson = JsonConvert.SerializeObject(content);
        var httpContent = new StringContent(contentJson, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(requestUri, httpContent);

        if (!response.IsSuccessStatusCode)
        {
            // Leia o conteúdo de erro como uma string
            var errorContent = await response.Content.ReadAsStringAsync();

            // Retorne a resposta com o conteúdo de erro
            return new HttpResponseMessage
            {
                StatusCode = response.StatusCode,
                ReasonPhrase = response.ReasonPhrase,
                Content = new StringContent(errorContent, Encoding.UTF8, "application/json")
            };
        }

        // Retorne a resposta bem-sucedida
        return response;
    }



    public async Task DeleteAsync(string requestUri)
    {
        var response = await _httpClient.DeleteAsync(requestUri);
        response.EnsureSuccessStatusCode();
    }

    public TService CreateClient<TService>() where TService : class
    {
        // Atribua uma instância do HttpClient ao serviço. Isto pode não funcionar
        // da maneira que você espera, já que geralmente um HttpClient é usado diretamente
        // para fazer chamadas HTTP, e não é passado para uma interface de serviço.
        var serviceInstance = Activator.CreateInstance(typeof(TService), _httpClient);

        if (serviceInstance is TService service)
        {
            return service;
        }

        throw new InvalidOperationException($"Cannot create client for type {typeof(TService).FullName}");
    }
}
