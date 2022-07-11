using Api.Models;
using Api.Models.DAO;
using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Resolvers;

/// <summary>
/// Class containing all possible query actions
/// </summary>
/// TODO: Separate actions for each entity in it's own separate class
public class Query {

    /// <summary> Query for all movies in the database </summary>
    public List<Movie> GetMovies([Service]MovieRepository repo) => repo.GetMovies;

    /// <summary> Query for a specific movie in the database </summary>
    /// <param name="id"> The id of the movie </param>
    public Movie GetMovieById([Service]MovieRepository repo, int id) => repo.GetMovieById(id);

    /// <summary> Query for all actors in the database </summary>
    public List<Actor> GetActors([Service]ActorRepository repo) => repo.GetActors;

    /// <summary> Query for a specific actor in the database </summary>
    /// <param name="id"> The id of the actor </param>
    public Actor GetActorById([Service]ActorRepository repo, int id) => repo.GetActorById(id);

    /// <summary> Query for all roles in the database </summary>
    public List<Role> GetRoles([Service]RoleRepository repo) => repo.GetRoles;

    /// <summary> Query for a specific role in the database </summary>
    /// <param name="id"> The id of the role </param>
    public Role GetRoleById([Service]RoleRepository repo, int id) => repo.GetRoleById(id);

    /// <summary> Query for all producers in the database </summary>
    public List<Producer> GetProducers([Service]ProducerRepository repo) => repo.GetProducers;

    /// <summary> Query for a specific producer in the database </summary>
    /// <param name="id"> The id of the producer </param>
    public Producer GetProducerById([Service]ProducerRepository repo, int id) => repo.GetProducerById(id);
}