using System.Text;
using Newtonsoft.Json;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T> GetAsync<T>(string requestUri)
    {
        var response = await _httpClient.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(content);
    }

    public async Task<T> PostAsync<T>(string requestUri, T content)
    {
        var contentJson = JsonConvert.SerializeObject(content);
        var httpContent = new StringContent(contentJson, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(requestUri, httpContent);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseContent);
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
