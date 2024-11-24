using System.ComponentModel.DataAnnotations.Schema;
using static System.String;

namespace DAS.GoT.Types.Entities;

/// <summary>
/// 
/// </summary>
[Table("Books")]
public class Book
{
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Path { get; set; } = Empty;
}