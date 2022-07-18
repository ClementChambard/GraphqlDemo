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

    /// <summary> 
    /// The users to put in the database during its initialisation 
    /// </summary>
    public static List<Auth.User> SeedUsers = new List<Auth.User>
    {
        new Auth.User{UserId = 1, FirstName = "ADMIN", LastName = "X", EmailAddress = "admin@api.com", Password = Auth.AuthLogic.PasswordHash("Test$001")},
        new Auth.User{UserId = 2, FirstName = "USER", LastName = "X", EmailAddress = "user@test.com", Password = Auth.AuthLogic.PasswordHash("Aaaa**00")}
    };

    /// <summary> 
    /// The users roles to put in the database during its initialisation 
    /// </summary>
    public static List<Auth.UserRoles> SeedUserRoles = new List<Auth.UserRoles>
    {
        new Auth.UserRoles{RoleId = 1, UserId = 1, Name = "admin"},
        new Auth.UserRoles{RoleId = 2, UserId = 2, Name = "default"}
    };
}