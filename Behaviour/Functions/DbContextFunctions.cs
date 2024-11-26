using DAS.GoT.Behaviour.Services;
using DAS.GoT.Types.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace DAS.GoT.Behaviour.Functions;

/// <summary>
/// 
/// </summary>
public static class DbContextFunctions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public static async Task<bool> HasEntitiesAsync(this PersonContext ctx, CancellationToken ct) => await ctx.Persons!.AnyAsync(ct);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="characters"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public static async Task<int> LoadAsync(this PersonContext ctx, IEnumerable<Character> characters, CancellationToken ct)
    {
        foreach(var character in characters)
        {
            var path = character.Url[character.Url.IndexOf("api/")..];
            bool shouldPersist = true;
            if(await ctx.HasEntitiesAsync(ct))
            {
                shouldPersist = !ctx.Persons!.Any(p => p.Path.Contains(path));
            }

            if(shouldPersist)
            {
                // ToDo: check on returned EntityEntries
                _ = ctx.Add(character.AsPerson());
            }
        }
        return await ctx.SaveChangesAsync(ct);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="store"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static async Task<int> SyncAsync(this PersonContext ctx, ICoreStore store, CancellationToken ct)
    {
        if(ctx.Persons is object && await ctx.HasEntitiesAsync(ct))
        {
            var values = ctx.Persons.Include(p => p.Aliases).Select(p => p.AsCharacter());
            store.AddMany(values);
        }
        else
        {
            throw new InvalidOperationException("Got no characters from database");
        }
        return store.Characters.Count();
    }
}