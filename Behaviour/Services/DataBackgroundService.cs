using DAS.GoT.Behaviour.Functions;
using DAS.GoT.Types.Models;
using DAS.GoT.Types.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static DAS.GoT.Behaviour.Functions.DbContextFunctions;
using static DAS.GoT.Behaviour.Functions.HttpRequestFunctions;

namespace DAS.GoT.Behaviour.Services;

/// <summary>
/// 
/// </summary>
/// <param name="logger"></param>
/// <param name="store"></param>
/// <param name="setup"></param>
/// <param name="serviceProvider"></param>
/// <param name="clientFactory"></param>
public class DataBackgroundService(
    ILogger<DataBackgroundService> logger,
    ICoreStore store,
    IOptions<ServerSetup> setup,
    IServiceProvider serviceProvider,
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
            var dbContext = serviceProvider.GetContext<PersonContext>();

            while(!ct.IsCancellationRequested)
            {
                var response = await clientFactory.WithClient().SendAsync(setup.WithRequest(), ct);
                if(response.IsSuccessStatusCode && await response.HasJsonContentAsync(ct))
                {
                    var characters = await response.SerializeAsync<IEnumerable<Character>>(ct);
                    _ = await store.LoadAsync(characters, ct);
                    _ = await dbContext.LoadAsync(characters, ct);
                }
                else
                {
                    _ = await dbContext.SyncAsync(store, ct);
                }
                await setup.Value.WaitForNextPollingAsync(ct);
            }
        }
        // ToDo: differentiate on exception types
        catch(Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }
    }
}
