using DAS.GoT.Types.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;
using Notification = DAS.GoT.Types.Messages.AddCharacterNotification;

namespace DAS.GoT.Behaviour.Consumers;

/// <summary>
/// 
/// </summary>
public class AddCharacterNotificationConsumer(
    ILogger<AddCharacterNotificationConsumer> logger) : IConsumer<AddCharacterNotification>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task Consume(ConsumeContext<Notification> context)
    {
        try
        {
            // ToDo: implement the actual role of this consumer...
            logger.LogInformation($"{typeof(AddCharacterNotificationConsumer).Name} got notified, [{DateTime.UtcNow}]");
            await Task.Delay(1200);
        }
        // ToDo: differentiate on exception types
        catch(Exception ex)
        {
            logger.LogError($"{ex.Message}, [{DateTime.UtcNow}]");
            throw new Exception(ex.Message, ex);
        }
    }
}
