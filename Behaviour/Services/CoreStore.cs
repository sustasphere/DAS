using DAS.GoT.Types.Models;
using static System.String;

namespace DAS.GoT.Behaviour.Services;

/// <summary>
/// 
/// </summary>
public class CoreStore : ICoreStore
{
    /// <summary>
    /// 
    /// </summary>
    public Dictionary<string, CharacterCore> Characters { get; } = [];

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty() => Characters.Count == 0;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool HasIdentical(CharacterCore value) => Characters.Values.Any(c => c.Equals(value));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public bool HasIdentical(IEnumerable<CharacterCore> values)
    {
        (var isLoaded, var equalCount, var flags) = (!IsEmpty(), false, new List<bool>());
        if(isLoaded)
        {
            equalCount = values.Count().Equals(Characters.Count);

            if(equalCount)
            {
                foreach(var character in values)
                {
                    flags.Add(Characters.Values.Any(v => v.Id.Equals(character.Id)));
                }
            }
        }
        return isLoaded && equalCount && flags.All(flag => flag.Equals(true));
    }

    /// <summary>
    /// 
    /// </summary>
    public void RemoveAll()
    {
        foreach(var key in Characters.Keys)
        {
            Characters.Remove(key);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <exception cref="ArgumentException"></exception>
    public void Add(Character value)
    {
        if(value.Url is object && value.Url.Contains("/"))
        {
            var key = value.Url.Split("/")!.Last();
            if(!Characters.ContainsKey(key))
            {
                Characters.Add(key, value.AsCore());
            }
        }
        else
        {
            throw new ArgumentException("Given url is invalid; should contain '/'", value.Url);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="values"></param>
    public void AddMany(IEnumerable<Character> values)
    {
        foreach(var value in values)
        {
            Add(value);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="values"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<int> LoadAsync(IEnumerable<Character> values, CancellationToken ct)
    {
        var producer = new TaskCompletionSource<int>(ct);
        if(IsEmpty())
        {
            AddMany(values);
            producer.SetResult(Characters.Count);
        }
        else
        {
            if(!HasIdentical(values.Select(c => c.AsCore())))
            {
                // ToDo: improve removal and / or adding of characters
                RemoveAll();
                AddMany(values);
                producer.SetResult(Characters.Count);
            }
            else
            {
                producer.SetResult(0);
            }
        }
        return await producer.Task;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public CharacterCore Get(string key)
    {
        if(IsNullOrEmpty(key))
        {
            throw new ArgumentException("Given key is invalid; should not be null or empty", key);
        }
        else
        {
            if(key.Contains("/"))
            {
                key = key.Split("/")!.Last();
            }
        }
        return Characters.ContainsKey(key) ? Characters[key] : new();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<CharacterCore> GetAll() => Characters.Values;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="alias"></param>
    /// <returns></returns>
    public IEnumerable<CharacterCore> Search(string alias)
        => Characters.Values.Where(c => c.Alias.Contains(alias, StringComparison.OrdinalIgnoreCase));


}
