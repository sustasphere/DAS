using System.ComponentModel.DataAnnotations.Schema;
using static System.String;

namespace DAS.GoT.Types.Entities;

/// <summary>
/// 
/// </summary>
[Table("TvSeries")]
public class TvSerie
{
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; } = Empty;
}
