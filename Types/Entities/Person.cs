using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.String;

namespace DAS.GoT.Types.Entities;

/// <summary>
/// 
/// </summary>
[Table("Persons")]
public class Person
{
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [Required] public string Path { get; set; } = Empty;
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; } = Empty;
    /// <summary>
    /// 
    /// </summary>
    // ToDo: map to GenderType
    public string Gender { get; set; } = Empty;
    /// <summary>
    /// 
    /// </summary>
    public string Culture { get; set; } = Empty;
    /// <summary>
    /// 
    /// </summary>
    // ToDo: add many to many relationship for mother parent
    public string Mother { get; set; } = Empty;
    /// <summary>
    /// 
    /// </summary>
    // ToDo: add many to many relationship for father parent
    public string Father { get; set; } = Empty;
    /// <summary>
    /// 
    /// </summary>
    // ToDo: add many to many relationship for spouse
    public string Spouse { get; set; } = Empty;
    /// <summary>
    /// 
    /// </summary>
    // ToDo: map to DateTime
    public string Born { get; set; } = Empty;
    /// <summary>
    /// 
    /// </summary>
    // ToDo: map to DateTime
    public string Died { get; set; } = Empty;
    /// <summary>
    /// 
    /// </summary>
    public List<Alias> Aliases { get; set; } = [];
    /// <summary>
    /// 
    /// </summary>
    public List<Title> Titles { get; set; } = [];
    /// <summary>
    /// 
    /// </summary>
    public List<Allegiance> Allegiances { get; set; } = [];
    /// <summary>
    /// 
    /// </summary>
    public List<Book> Books { get; set; } = [];
    /// <summary>
    /// 
    /// </summary>
    public List<PovBook> PovBooks { get; set; } = [];
    /// <summary>
    /// 
    /// </summary>
    public List<TvSerie> TvSeries { get; set; } = [];
    /// <summary>
    /// 
    /// </summary>
    public List<Player> PlayedBy { get; set; } = [];
}
