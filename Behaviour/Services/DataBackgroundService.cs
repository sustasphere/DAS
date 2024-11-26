using DAS.GoT.Types.Models;
using DAS.GoT.Types.Utils;
using MassTransit.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using static DAS.GoT.Behaviour.Functions.HttpRequestFunctions;

namespace DAS.GoT.Behaviour.Services;

/// <summary>
/// 
/// </summary>
/// <param name="logger"></param>
/// <param name="store"></param>
/// <param name="setup"></param>
/// <param name="provider"></param>
/// <param name="clientFactory"></param>
public class DataBackgroundService(
    ILogger<DataBackgroundService> logger,
    ICoreStore store,
    IOptions<ServerSetup> setup,
    IServiceProvider provider,
    IHttpClientFactory clientFactory) : BackgroundService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        try
        {
            using var scope = provider!.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<PersonContext>();
            var personsSet = dbContext.Persons;
            var hasPersistedPersons = await personsSet!.AnyAsync(ct);

            while(!ct.IsCancellationRequested)
            {
                var responseMessage = await clientFactory.WithClient().SendAsync(setup.WithRequest(), ct);
                if(responseMessage!.IsSuccessStatusCode)
                {
                    using var textTask = responseMessage.Content.ReadAsStringAsync(ct);
                    var text = await textTask;
                    if(text.StartsWith("<"))
                    {
                        throw new InvalidOperationException("Got XML or HTML!");
                    }
                    else
                    {
                        using var stream = await responseMessage.Content?.ReadAsStreamAsync(ct)!;
                        var characters = await JsonSerializer.DeserializeAsync<IEnumerable<Character>>(stream, WithSerializerOptions(), ct);
                        if(characters is object && characters.Any())
                        {
                            if(store.IsEmpty())
                            {
                                store.AddMany(characters);
                            }
                            else
                            {
                                if(!store.HasIdentical(characters.Select(c => c.AsCore())))
                                {
                                    // ToDo: improve removal and / or adding of characters
                                    store.RemoveAll();
                                    store.AddMany(characters);
                                }
                            }

                            foreach(var character in characters)
                            {
                                var path = character.Url[character.Url.IndexOf("api/")..];
                                bool shouldPersist = true;
                                if(hasPersistedPersons)
                                {
                                    shouldPersist = !personsSet!.Any(p => p.Path.Contains(path));
                                }

                                if(shouldPersist)
                                {
                                    _ = dbContext.Add(character.AsPerson());
                                }
                            }
                            _ = await dbContext.SaveChangesAsync(ct);
                        }
                    }
                }
                else
                {
                    logger.LogError($"Unable to retrieve characters from host; [{responseMessage.StatusCode}]");
                    if(hasPersistedPersons)
                    {
                        var characters = dbContext!.Persons!.Include(p => p.Aliases).Select(p => p.AsCharacter()).ToList();
                        store.AddMany(characters);
                    }
                    else
                    {
                        logger.LogError($"Unable to retrieve characters from database either");
                    }
                }
                await Task.Delay(TimeSpan.FromMinutes(setup.Value.PollingDelayMinutes), ct);
            }
        }
        // ToDo: differentiate on exception types
        catch(Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }
    }
}
