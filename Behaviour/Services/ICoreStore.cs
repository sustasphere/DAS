using DAS.GoT.Types.Models;

namespace DAS.GoT.Behaviour.Services;

/// <summary>
/// 
/// </summary>
public interface ICoreStore
{
    /// <summary>
    /// 
    /// </summary>
    Dictionary<string, CharacterCore> Characters { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    bool IsEmpty();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    bool HasIdentical(CharacterCore value);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    bool HasIdentical(IEnumerable<CharacterCore> values);

    /// <summary>
    /// 
    /// </summary>
    void RemoveAll();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    void Add(Character value);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="values"></param>
    void AddMany(IEnumerable<Character> values);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="values"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<int> LoadAsync(IEnumerable<Character> values, CancellationToken ct);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    CharacterCore Get(string key);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerable<CharacterCore> GetAll();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="alias"></param>
    /// <returns></returns>
    IEnumerable<CharacterCore> Search(string alias);
}