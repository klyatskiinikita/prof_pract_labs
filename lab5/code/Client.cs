using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiHttpClient;

public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializerOptions;

    public ApiClient(string baseUrl)
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<ApiResponse<T>> GetAsync<T>(string endpoint)
    {
        try
        {
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<T>>(content, _serializerOptions);

            return new ApiResponse<T>
            {
                StatusCode = response.StatusCode,
                Data = data
            };
        }
        catch (HttpRequestException ex)
        {
            return new ApiResponse<T>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }

    public async Task<ApiResponse<T>> PostAsync<T>(string endpoint, Dictionary<string, string> parameters)
    {
        try
        {
            var response = await _httpClient.PostAsync(endpoint, new FormUrlEncodedContent(parameters));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<T>>(content, _serializerOptions);

            return new ApiResponse<T>
            {
                StatusCode = response.StatusCode,
                Data = data
            };
        }
        catch (HttpRequestException ex)
        {
            return new ApiResponse<T>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }
}
public class ApiResponse<T>
{
    public string Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public List<T> Data { get; set; }
}
