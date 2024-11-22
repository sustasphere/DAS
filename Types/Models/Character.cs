using static System.String;

namespace DAS.GoT.Types.Models;

/// <summary>
/// A Character is an individual within the Ice And Fire universe.
/// </summary>
public record Character
{
    /// <summary>
    /// The hypermedia URL of this resource
    /// </summary>
    public string Url { get; init; } = Empty;
    /// <summary>
    /// The name of this character
    /// </summary>
    public string Name { get; init; } = Empty;
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
    /// Textual representation of when and where this character died.
    /// </summary>
    public string Died { get; init; } = Empty;
    /// <summary>
    /// The titles that this character holds.
    /// </summary>
    public string[] Titles { get; init; } = [];
    /// <summary>
    /// The aliases that this character goes by.
    /// </summary>
    public string[] Aliases { get; init; } = [];
    /// <summary>
    /// The character resource URL of this character's father.
    /// </summary>
    public string Father { get; init; } = Empty;
    /// <summary>
    /// The character resource URL of this character's mother.
    /// </summary>
    public string Mother { get; init; } = Empty;
    /// <summary>
    /// An array of Character resource URLs that has had a POV-chapter in this book.
    /// </summary>
    public string Spouse { get; init; } = Empty;
    /// <summary>
    /// An array of House resource URLs that this character is loyal to.
    /// </summary>
    public string[] Allegiances { get; init; } = [];
    /// <summary>
    /// An array of Book resource URLs that this character has been in.
    /// </summary>
    public string[] Books { get; init; } = [];
    /// <summary>
    /// An array of Book resource URLs that this character has had a POV-chapter in.
    /// </summary>
    public string[] PovBooks { get; init; } = [];
    /// <summary>
    /// An array of names of the seasons of Game of Thrones that this character has been in.
    /// </summary>
    public string[] TvSeries { get; init; } = [];
    /// <summary>
    /// An array of actor names that has played this character in the TV show Game Of Thrones.
    /// </summary>
    public string[] PlayedBy { get; init; } = [];
}
