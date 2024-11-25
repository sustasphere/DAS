using DAS.GoT.Types.Models;
using static System.String;

namespace DAS.GoT.Types.Messages;

/// <summary>
/// 
/// </summary>
public record AddCharacterResult
{
    /// <summary>
    /// 
    /// </summary>
    public Guid CorrelationId { get; init; } = Guid.Empty;

    /// <summary>
    /// 
    /// </summary>
    public CharacterCore Value { get; private set; } = new();

    /// <summary>
    /// 
    /// </summary>
    public bool HasSuccess { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public string Message { get; private set; } = Empty;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="correlationId"></param>
    /// <returns></returns>
    public static AddCharacterResult Create(Guid correlationId)
        => new() { CorrelationId = correlationId };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public static AddCharacterResult Create(string guid)
    {
        if(!IsNullOrEmpty(guid) && Guid.TryParse(guid, out Guid correlationId))
        {
            return new() { CorrelationId = correlationId };
        }
        else
        {
            return new() {
                CorrelationId = Guid.Empty,
                Message = $"Given guid {guid} does not have a proper guid-format"
            };
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public AddCharacterResult WithSuccess(CharacterCore value)
    {
        Value = value;
        HasSuccess = true;
        Message = "OK";
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public AddCharacterResult WithFailure(string message = "ERROR")
    {
        HasSuccess = false;
        Message = IsNullOrEmpty(Message) ? message : $"[{message}] {Message}";
        return this;
    }
}
