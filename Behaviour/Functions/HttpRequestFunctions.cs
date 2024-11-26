using DAS.GoT.Types.Models;
using DAS.GoT.Types.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Runtime.CompilerServices;
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
    /// <param name="setup"></param>
    /// <returns></returns>
    public static string WithUrl(this IOptions<ServerSetup> setup)
        => $"https://www.{setup.Value.Host}/api/{setup.Value.Path}";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="clientFactory"></param>
    /// <returns></returns>
    public static HttpClient WithClient(this IHttpClientFactory clientFactory) => clientFactory.CreateClient();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="setup"></param>
    /// <returns></returns>
    public static HttpRequestMessage WithRequest(this IOptions<ServerSetup> setup)
        => new HttpRequestMessage(HttpMethod.Get, setup.WithUrl()) {
            Content = default,
            Headers = {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.UserAgent, "WebApi Client" }
                }
        };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="response"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static async Task<bool> HasJsonContentAsync(this HttpResponseMessage response, CancellationToken ct)
    {
        using var textTask = response.Content.ReadAsStringAsync(ct);
        if((await textTask).StartsWith("<"))
        {
            throw new InvalidOperationException("Got XML or HTML from server");
        }
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="response"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static async Task<IEnumerable<Character>> AsCharactersAsync(this HttpResponseMessage response, CancellationToken ct)
    {
        using var stream = await response.Content.ReadAsStreamAsync(ct)!;
        var characters = await JsonSerializer.DeserializeAsync<IEnumerable<Character>>(stream, WithSerializerOptions(), ct);

        if(characters is object && characters.Any())
        {
            return characters;
        }
        else
        {
            throw new InvalidOperationException("Got no characters from server");
        }
    }
}
