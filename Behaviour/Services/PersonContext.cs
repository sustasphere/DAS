using DAS.GoT.Types.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAS.GoT.Behaviour.Services;

/// <summary>
/// 
/// </summary>
public class PersonContext : DbContext
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public PersonContext(DbContextOptions<PersonContext> options) : base(options) { }

    /// <summary>
    /// 
    /// </summary>
    public DbSet<Person>? Persons { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Person>().HasMany(e => e.TvSeries).WithMany();
        builder.Entity<Person>().HasMany(e => e.Titles).WithMany();
        builder.Entity<Person>().HasMany(e => e.PovBooks).WithMany();
        builder.Entity<Person>().HasMany(e => e.PlayedBy).WithMany();
        builder.Entity<Person>().HasMany(e => e.Allegiances).WithMany();
        builder.Entity<Person>().HasMany(e => e.Books).WithMany();
    }
}
