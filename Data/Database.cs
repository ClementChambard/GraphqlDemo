using Api.Data.Models;

namespace Api.Data;

class Database {

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
                context.SaveChanges();
            }
        }
    }

}