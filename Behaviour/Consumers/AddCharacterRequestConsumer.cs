using DAS.GoT.Behaviour.Services;
using DAS.GoT.Types.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;
using Notification = DAS.GoT.Types.Messages.AddCharacterNotification;
using Result = DAS.GoT.Types.Messages.AddCharacterResult;

namespace DAS.GoT.Behaviour.Consumers;

/// <summary>
/// 
/// </summary>
public class AddCharacterRequestConsumer(
    PersonContext dbContext,
    ILogger<AddCharacterRequestConsumer> logger) : IConsumer<AddCharacterRequest>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ctx"></param>
    /// <returns></returns>
    public async Task Consume(ConsumeContext<AddCharacterRequest> ctx)
    {
        (var withId, var value) = (ctx.Message.CorrelationId, ctx.Message.Character);
        using var transaction = dbContext.Database.BeginTransaction();
        try
        {
            _ = dbContext.Add(value.AsPerson());

            _ = await dbContext.SaveChangesAsync();

            await ctx.Publish<AddCharacterNotification>(Notification.Create(value, withId));

            transaction.Commit();

            await ctx.RespondAsync<AddCharacterResult>(
                Result.Create(withId).WithSuccess(value.AsCore())
                );
        }
        // ToDo: differentiate on exception types
        catch(Exception ex)
        {
            logger.LogError($"{ex.Message}, [{DateTime.UtcNow}]");

            await ctx.RespondAsync<AddCharacterResult>(
                Result.Create(withId).WithFailure(ex.Message)
                );
        }
    }
}
