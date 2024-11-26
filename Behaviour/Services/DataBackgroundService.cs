using DAS.GoT.Behaviour.Functions;
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

            while(!ct.IsCancellationRequested)
            {
                var responseMessage = await clientFactory.WithClient().SendAsync(setup.WithRequest(), ct);
                if(responseMessage.IsSuccessStatusCode && await responseMessage.HasJsonContentAsync(ct))
                {
                    var characters = await responseMessage.AsCharactersAsync(ct);
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
