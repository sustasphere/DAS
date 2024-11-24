using DAS.GoT.Types.Entities;
using static System.String;

namespace DAS.GoT.Types.Models;

/// <summary>
/// A Character is an individual within the Ice And Fire universe.
/// </summary>
public class Character : IEquatable<Character>
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

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Person AsPerson() => new() {
        Path = Url[Url.IndexOf("api/")..],
        Name = Name,
        Gender = Gender,
        Culture = Culture,
        Born = Born,
        Died = Died,
        Father = Father,
        Mother = Mother,
        Spouse = Spouse,
        Aliases = new List<Alias>(Aliases.Select(text => new Alias() { Name = text })),
        TvSeries = new List<TvSerie>(TvSeries.Select(text => new TvSerie() { Name = text })),
        Titles = new List<Title>(Titles.Select(text => new Title() { Name = text })),
        PovBooks = new List<PovBook>(PovBooks.Select(text => new PovBook() { Url = text })),
        PlayedBy = new List<Player>(PlayedBy.Select(text => new Player() { Name = text })),
        Allegiances = new List<Allegiance>(Allegiances.Select(text => new Allegiance() { Path = text })),
        Books = new List<Book>(Books.Select(text => new Book() { Path = text }))
    };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj) => this.Equals(obj as Character);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Character? other)
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
    public override int GetHashCode() => (Url?.Split("/")?.Last(), Join(", ", Aliases), Gender, Born).GetHashCode();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    public static bool operator ==(Character lhs, Character rhs)
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
    public static bool operator !=(Character lhs, Character rhs) => !(lhs == rhs);
}
