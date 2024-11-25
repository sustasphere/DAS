namespace DAS.GoT.Types.Models;
/// <summary>
/// 
/// </summary>
public class Paged<TResult> where TResult : class
{
    /// <summary>
    /// 
    /// </summary>
    public int TotalItems { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    public int TotalPages { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public Dictionary<int, TResult> Pages { get; private set; } = [];

    /// <summary>
    /// 
    /// </summary>
    /// <param name="values"></param>
    /// <param name="itemsPerPage"></param>
    /// <returns></returns>
    public static Paged<TResult> Create(IEnumerable<TResult> values, int itemsPerPage)
    {
        decimal totalItems = values.Count();
        if(itemsPerPage > 0)
        {
            return new() {
                TotalItems = (int)totalItems,
                TotalPages = (int)Math.Max(1, Math.Ceiling(totalItems / itemsPerPage)),
                Pages = new Dictionary<int, TResult>(values.Select(
                    (v, idx) => new KeyValuePair<int, TResult>(idx, v))
                )
            };
        }
        else
        {
            throw new ArgumentException("Items per page can't be zero");
        }
    }
}
