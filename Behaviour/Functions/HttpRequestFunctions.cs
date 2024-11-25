using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DAS.GoT.Behaviour.Functions;

/// <summary>
/// 
/// </summary>
public static class HttpRequestFunctions
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static JsonSerializerOptions WithSerializerOptions() => new() {
        AllowTrailingCommas = true,
        PreferredObjectCreationHandling = JsonObjectCreationHandling.Populate,
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static string WithUrl(this IConfiguration configuration)
    {
        // ToDo: load the host and data from app-settings
        var host = "anapioficeandfire.com";
        var data = "characters";
        return $"https://www.{host}/api/{data}";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="clientFactory"></param>
    /// <returns></returns>
    public static HttpClient WithClient(this IHttpClientFactory clientFactory) => clientFactory.CreateClient();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static HttpRequestMessage WithRequest(this IConfiguration configuration)
        => new HttpRequestMessage(HttpMethod.Get, configuration.WithUrl()) {
            Content = default,
            Headers = {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.UserAgent, "WebApi Client" }
                }
        };
}
