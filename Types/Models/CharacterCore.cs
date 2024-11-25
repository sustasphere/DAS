using static System.String;

namespace DAS.GoT.Types.Models;

/// <summary>
/// 
/// </summary>
public class CharacterCore : IEquatable<CharacterCore>
{
    /// <summary>
    /// The generated guid
    /// </summary>
    public Guid Id { get; init; } = Guid.Empty;
    /// <summary>
    /// The gender of this character
    /// </summary>
    public string Gender { get; init; } = Empty;
    /// <summary>
    /// The culture that this character belongs to.
    /// </summary>
    public string Culture { get; init; } = Empty;
    /// <summary>
    /// Textual representation of when and where this character was born.
    /// </summary>
    public string Born { get; init; } = Empty;
    /// <summary>
    /// The first main alias that this character goes by.
    /// </summary>
    public string Alias { get; init; } = Empty;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj) => this.Equals(obj as CharacterCore);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(CharacterCore? other)
    {
        if(other is object)
        {
            if(!ReferenceEquals(this, other)) return false;
            if(this.GetType() != other.GetType()) return false;
            return this.GetHashCode() == other.GetHashCode();
        }
        else return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => (Alias, Gender, Culture, Born).GetHashCode();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    public static bool operator ==(CharacterCore lhs, CharacterCore rhs)
    {
        if(lhs is null)
        {
            return rhs is null;
        }
        else return lhs.Equals(rhs);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    public static bool operator !=(CharacterCore lhs, CharacterCore rhs) => !(lhs == rhs);
}