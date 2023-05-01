using System.Net;
using ApiHttpClient;
var apiClient = new ApiClient("https://od-api-demo.oxforddictionaries.com:443/api/v1");
var response = await apiClient.GetAsync<string>("/languages");
if (response.StatusCode == HttpStatusCode.OK)
{
    foreach (var language in response.Data)
    {
        Console.WriteLine(language);
    }
}
else
{
    Console.WriteLine($"Error: {response.Message}");
}
