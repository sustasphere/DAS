using DAS.GoT.Types.Models;

namespace DAS.GoT.Types.Messages;

/// <summary>
/// 
/// </summary>
public record AddCharacterNotification
{
    /// <summary>
    /// 
    /// </summary>
    public Guid CorrelationId { get; init; } = Guid.Empty;

    /// <summary>
    /// 
    /// </summary>
    public Character Value { get; init; } = new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="correlationId"></param>
    /// <returns></returns>
    public static AddCharacterNotification Create(Character value, Guid correlationId) => new() {
        // ToDo: consider adding basic parameter validation
        Value = value,
        CorrelationId = correlationId
    };
}