using DAS.GoT.Types.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DAS.GoT.Behaviour.Services;

/// <summary>
/// 
/// </summary>
/// <param name="logger"></param>
/// <param name="dbContext"></param>
/// <param name="clientFactory"></param>
public class DataBackgroundService(
    ILogger<DataBackgroundService> logger,
    IHttpClientFactory clientFactory) : BackgroundService
{
    /// <summary>
    /// 
    /// </summary>
    public IServiceProvider? Provider { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        var url = "https://www.anapioficeandfire.com/api/characters";
        var client = clientFactory.CreateClient();
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, url) {
            Content = default,
            Headers = {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.UserAgent, "WebApi Client" }
                }
        };

        using var scope = Provider!.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PersonContext>();
        var personsSet = dbContext.Persons;
        var hasPersistedPersons = await personsSet!.AnyAsync(ct);

        while(!ct.IsCancellationRequested)
        {
            var responseMessage = await client.SendAsync(requestMessage, ct);
            if(responseMessage!.IsSuccessStatusCode)
            {
                using var textTask = responseMessage.Content.ReadAsStringAsync(ct);
                var text = await textTask;
                if(text.StartsWith("<"))
                {
                    var mess = "Got XML or HTML!";
                    logger.LogError(mess);
                    throw new Exception(mess);
                }
                else
                {
                    using var stream = await responseMessage.Content?.ReadAsStreamAsync(ct)!;
                    var result = await JsonSerializer.DeserializeAsync<IEnumerable<Character>>(stream, new JsonSerializerOptions() {
                        AllowTrailingCommas = true,
                        PreferredObjectCreationHandling = JsonObjectCreationHandling.Populate,
                        PropertyNameCaseInsensitive = true
                    });
                    if(result is object && result.Any())
                    {
                        foreach(var character in result.Take(3))
                        {
                            var path = character.Url.Substring(character.Url.IndexOf("api"));
                            bool isKnown = false;
                            if(hasPersistedPersons)
                            {
                                isKnown = personsSet!.Any(p => p.Path.Contains(path));
                            }

                            if(!isKnown)
                            {
                                dbContext.Add(character.AsPerson());
                            }
                        }
                        await dbContext.SaveChangesAsync(ct);
                    }
                }
            }
            await Task.Delay(TimeSpan.FromMinutes(12), ct);
        }
    }
}
