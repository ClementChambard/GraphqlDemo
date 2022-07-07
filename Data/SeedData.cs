using Api.Models;

namespace Api.Data;

/// <summary>
/// Class containing seed data for the database
/// </summary>
public class Seed
{
    /// <summary> 
    /// The actors to put in the database during its initialisation 
    /// </summary>
    public static List<Actor> SeedActors = new List<Actor>
    {
        new Actor{Id = 1, FirstName = "Bob", LastName = "Kante"},
        new Actor{Id = 2, FirstName = "Mary", LastName = "Poppins"}
    };

    /// <summary> 
    /// The producers to put in the database during its initialisation 
    /// </summary>
    public static List<Producer> SeedProducers = new List<Producer>
    {
        new Producer{Id = 1, FirstName = "P", LastName = "Roducer"}
    };

    /// <summary> 
    /// The movies to put in the database during its initialisation 
    /// </summary>
    public static List<Movie> SeedMovies = new List<Movie>
    {
        new Movie{Id = 1, Title = "The Rise of the GraphQL Warrior", Actors = new List<Actor>(SeedActors), MovieProducer = SeedProducers[0]},
        new Movie{Id = 2, Title = "The Rise of the GraphQL Warrior Part 2", Actors = new List<Actor>(SeedActors), MovieProducer = SeedProducers[0]}
    };

    /// <summary> 
    /// The roles to put in the database during its initialisation 
    /// </summary>
    public static List<Role> SeedRoles = new List<Role>
    {
        new Role{Id = 1, Name = "The programmer", RoleActor = SeedActors[0], RoleMovie = SeedMovies[0]},
        new Role{Id = 2, Name = "The tester", RoleActor = SeedActors[1], RoleMovie = SeedMovies[0]},
        new Role{Id = 3, Name = "The programmer", RoleActor = SeedActors[0], RoleMovie = SeedMovies[1]},
        new Role{Id = 4, Name = "The hacker", RoleActor = SeedActors[1], RoleMovie = SeedMovies[1]}
    };
}