using Api.Auth;

namespace Api.Data;

/// <summary>
/// Class for some database interactions
/// </summary>
class Database {

    /// <summary>
    /// Initializes the database with seed data if it has not been created
    /// </summary>
    /// <param name="app"> the app containing the database context </param>
    public static void Init(IApplicationBuilder app) {
        using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<ApiDbContext>();
            if (context.Database.EnsureCreated())
            {
                context.Actors.AddRange(Seed.SeedActors);
                context.Movies.AddRange(Seed.SeedMovies);
                context.Producers.AddRange(Seed.SeedProducers);
                context.Roles.AddRange(Seed.SeedRoles);
                context.Users.AddRange(Seed.SeedUsers);
                context.UserRoles.AddRange(Seed.SeedUserRoles);
                context.SaveChanges();
            }
        }
    }

}