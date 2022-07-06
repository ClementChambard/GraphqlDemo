using Api.Data.Models;

namespace Api.Data;

class Database {

    public static void Init(ApiDbContext context) {
        context.Actors.AddRange(Seed.SeedActors);
        context.Movies.AddRange(Seed.SeedMovies);
        context.Producers.AddRange(Seed.SeedProducers);
        context.Roles.AddRange(Seed.SeedRoles);
        context.SaveChanges();
    }

}