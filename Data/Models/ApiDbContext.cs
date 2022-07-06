using Microsoft.EntityFrameworkCore;

namespace Api.Data.Models;

public class ApiDbContext : DbContext {

    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) {}

    public DbSet<Actor> Actors { get; set; }
    public DbSet<Producer> Producers { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Role> Roles { get; set; }

}