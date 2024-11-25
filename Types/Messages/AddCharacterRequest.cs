using DAS.GoT.Types.Models;
using static System.String;

namespace DAS.GoT.Types.Messages;

/// <summary>
/// 
/// </summary>
public record AddCharacterRequest
{
    /// <summary>
    /// 
    /// </summary>
    public Guid CorrelationId { get; init; } = Guid.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string CorrId { get; init; } = Empty;

    /// <summary>
    /// 
    /// </summary>
    public Character Character { get; init; } = new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="character"></param>
    /// <param name="guid"></param>
    /// <returns></returns>
    public static AddCharacterRequest Create(Character character, string guid)
    {
        if(character is object)
        {
            if(Guid.TryParse(guid, out Guid correlationId))
            {
                return new() {
                    CorrelationId = correlationId,
                    Character = character,
                };
            }
            else
            {
                throw new ArgumentException("Given correlation id is not a proper guid");
            }
        }
        else
        {
            throw new ArgumentException("Given character is null");
        }
    }
}
