using Api.Data.Models;

namespace Api.Data;
public class Seed
{
    public static List<Actor> SeedActors = new List<Actor>
    {
        new Actor{Id = 1, FirstName = "Bob", LastName = "Kante"},
        new Actor{Id = 2, FirstName = "Mary", LastName = "Poppins"}
    };

    public static List<Producer> SeedProducers = new List<Producer>
    {
        new Producer{Id = 1, FirstName = "P", LastName = "Roducer"}
    };
    public static List<Movie> SeedMovies = new List<Movie>
    {
        new Movie{Id = 1, Title = "The Rise of the GraphQL Warrior", Actors = new List<Actor>(SeedActors), producer = SeedProducers[0]},
        new Movie{Id = 2, Title = "The Rise of the GraphQL Warrior Part 2", Actors = new List<Actor>(SeedActors), producer = SeedProducers[0]}
    };

    public static List<Role> SeedRoles = new List<Role>
    {
        new Role{Id = 1, Name = "The programmer", actor = SeedActors[0], movie = SeedMovies[0]},
        new Role{Id = 2, Name = "The tester", actor = SeedActors[1], movie = SeedMovies[0]},
        new Role{Id = 3, Name = "The programmer", actor = SeedActors[0], movie = SeedMovies[1]},
        new Role{Id = 4, Name = "The hacker", actor = SeedActors[1], movie = SeedMovies[1]}
    };
}