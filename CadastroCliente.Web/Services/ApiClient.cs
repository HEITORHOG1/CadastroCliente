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

        return await _httpClient.PostAsync(requestUri, httpContent);
    }

    public async Task PutAsync<T>(string requestUri, T content)
    {
        var contentJson = JsonConvert.SerializeObject(content);
        var httpContent = new StringContent(contentJson, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(requestUri, httpContent);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(string requestUri)
    {
        var response = await _httpClient.DeleteAsync(requestUri);
        response.EnsureSuccessStatusCode();
    }
}
