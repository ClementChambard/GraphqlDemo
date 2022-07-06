using Microsoft.EntityFrameworkCore;

namespace Api.Data.Models;

public class ApiDbContext : DbContext {

    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) {}

    public DbSet<Actor> Actors { get; set; }
    public DbSet<Producer> Producers { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        //options.UseInMemoryDatabase("ApiDatabase");
        options.UseSqlite("Data Source=Database.db");
    }

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<Movie>()
             .HasMany(t => t.Actors)
             .WithMany(t => t.Movies);
        model.Entity<Movie>()
             .HasOne(t => t.producer)
             .WithMany(t => t.Movies)
             .HasForeignKey(t => t.ProducerId);
        model.Entity<Movie>()
             .HasMany(t => t.Roles)
             .WithOne(t => t.movie)
             .HasForeignKey(t => t.MovieId);

        model.Entity<Actor>()
             .HasMany(t => t.Movies)
             .WithMany(t => t.Actors);
        model.Entity<Actor>()
             .HasMany(t => t.Roles)
             .WithOne(t => t.actor)
             .HasForeignKey(t => t.ActorId);

        model.Entity<Role>()
             .HasOne(t => t.actor)
             .WithMany(t => t.Roles)
             .HasForeignKey(t => t.ActorId);
        model.Entity<Role>()
             .HasOne(t => t.movie)
             .WithMany(t => t.Roles)
             .HasForeignKey(t => t.MovieId);

        model.Entity<Producer>()
             .HasMany(t => t.Movies)
             .WithOne(t => t.producer)
             .HasForeignKey(t => t.ProducerId);
    }

}