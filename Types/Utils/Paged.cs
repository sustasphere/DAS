namespace DAS.GoT.Types.Utils;
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
    public Dictionary<int, IEnumerable<TResult>> Pages { get; private set; } = [];

    /// <summary>
    /// 
    /// </summary>
    /// <param name="values"></param>
    /// <param name="itemsPerPage"></param>
    /// <returns></returns>
    public static Paged<TResult> Create(IEnumerable<TResult> values, int itemsPerPage)
    {
        decimal totalItems = values.Count();
        var pages = new Dictionary<int, IEnumerable<TResult>>();
        if(itemsPerPage > 0)
        {
            var totalPages = (int)Math.Max(1, Math.Ceiling(totalItems / itemsPerPage));
            for(int pageIdx = 0; pageIdx <= totalPages; pageIdx += itemsPerPage)
            {
                var page = values.Take(itemsPerPage);
                pages.Add(pageIdx, page);
                values = values.Skip(itemsPerPage);
            }
            return new() {
                TotalItems = (int)totalItems,
                TotalPages = totalPages,
                Pages = pages
            };
        }
        else
        {
            throw new ArgumentException("Items per page can't be zero");
        }
    }
}
