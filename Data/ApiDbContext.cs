using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Data;

/// <summary>
/// Database context of the api
/// </summary>
public class ApiDbContext : DbContext {

    /// <summary> 
    /// Initializes a new instance of the ApiDbContext class using the specified options. 
    /// The ApiDbContext.OnConfiguring(DbContextOptionsBuilder) method will still be called 
    /// to allow further configuration of the options. <br/> See DbContext for more 
    /// information.
    /// </summary>
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) {}

    /// <summary> The list of actors in the database </summary>
    public DbSet<Actor> Actors { get; set; }

    /// <summary> The list of producers in the database </summary>
    public DbSet<Producer> Producers { get; set; }

    /// <summary> The list of movies in the database </summary>
    public DbSet<Movie> Movies { get; set; }

    /// <summary> The list of roles in the database </summary>
    public DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Use this option if you want the database to be stored
        // in memory
        /* options.UseInMemoryDatabase("ApiDatabase"); */

        // Use this option if you want the database to be stored
        // in an external file
        options.UseSqlite("Data Source=Database.db");
    }

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<Movie>()
             .HasMany(t => t.Actors)
             .WithMany(t => t.Movies);
        model.Entity<Movie>()
             .HasOne(t => t.MovieProducer)
             .WithMany(t => t.Movies)
             .HasForeignKey(t => t.ProducerId);
        model.Entity<Movie>()
             .HasMany(t => t.Roles)
             .WithOne(t => t.RoleMovie)
             .HasForeignKey(t => t.MovieId);

        model.Entity<Actor>()
             .HasMany(t => t.Movies)
             .WithMany(t => t.Actors);
        model.Entity<Actor>()
             .HasMany(t => t.Roles)
             .WithOne(t => t.RoleActor)
             .HasForeignKey(t => t.ActorId);

        model.Entity<Role>()
             .HasOne(t => t.RoleActor)
             .WithMany(t => t.Roles)
             .HasForeignKey(t => t.ActorId);
        model.Entity<Role>()
             .HasOne(t => t.RoleMovie)
             .WithMany(t => t.Roles)
             .HasForeignKey(t => t.MovieId);

        model.Entity<Producer>()
             .HasMany(t => t.Movies)
             .WithOne(t => t.MovieProducer)
             .HasForeignKey(t => t.ProducerId);
    }

}