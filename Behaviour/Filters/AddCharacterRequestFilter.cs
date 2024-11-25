using DAS.GoT.Behaviour.Services;
using DAS.GoT.Types.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace DAS.GoT.Behaviour.Filters;

/// <summary>
/// 
/// </summary>
public class AddCharacterRequestFilter<TRequest>(
    ICoreStore store,
    ILogger<AddCharacterRequestFilter<TRequest>> logger) : IFilter<SendContext<TRequest>> where TRequest : class
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ctx"></param>
    public void Probe(ProbeContext ctx)
    {
        logger.LogDebug($"{typeof(AddCharacterRequestFilter<TRequest>).Name} got probed...");
        // ToDo: add probe set
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task Send(SendContext<TRequest> ctx, IPipe<SendContext<TRequest>> next)
    {
        if(ctx.Message is AddCharacterRequest request && request.CorrelationId != Guid.Empty)
        {
            var characterCore = request.Character.AsCore();
            if(store.HasIdentical(characterCore))
            {
                var mess = "Character already exists";
                logger.LogError($"{mess}, [{DateTime.UtcNow}]");
                throw new ArgumentException(mess);
            }
            // ToDo: add additional validation logic on: Url, Gender, Culture, Born and Alias
        }
        else
        {
            if(ctx.Message is AddCharacterResult result || ctx.Message is AddCharacterNotification notification)
            {
                // ToDo: consider adding additional post validation logic
            }
            else
            {
                var mess = "CorrelationId must not be empty";
                logger.LogError($"{mess}, [{DateTime.UtcNow}]");
                throw new ArgumentException(mess);
            }
        }
        await next.Send(ctx);
    }
}
