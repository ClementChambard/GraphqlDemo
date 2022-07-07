using Api.Models;
using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Resolvers;

/// <summary>
/// Class containing all possible query actions
/// </summary>
/// TODO: Separate actions for each entity in it's own separate class
public class Query {

    /// <summary> Query for all movies in the database </summary>
    public List<Movie> GetMovies([Service]ApiDbContext context) => context.Movies.Include(m => m.Actors).Include(m => m.MovieProducer).Include(m => m.Roles).ToList();

    /// <summary> Query for a specific movie in the database </summary>
    /// <param name="id"> The id of the movie </param>
    public Movie GetMovieById([Service]ApiDbContext context, int id) => context.Movies.Include(m => m.Actors).Include(m => m.MovieProducer).Include(m => m.Roles).FirstOrDefault(x => x.Id == id);

    /// <summary> Query for all roles in the database </summary>
    public List<Role> GetRoles([Service]ApiDbContext context) => context.Roles.Include(r => r.RoleActor).Include(r => r.RoleMovie).ToList();

    /// <summary> Query for a specific role in the database </summary>
    /// <param name="id"> The id of the role </param>
    public Role GetRoleById([Service]ApiDbContext context, int id) => context.Roles.Include(r => r.RoleActor).Include(r => r.RoleMovie).FirstOrDefault(x => x.Id == id);

    /// <summary> Query for all actors in the database </summary>
    public List<Actor> GetActors([Service]ApiDbContext context) => context.Actors.Include(a => a.Roles).Include(a => a.Movies).ToList();

    /// <summary> Query for a specific actor in the database </summary>
    /// <param name="id"> The id of the actor </param>
    public Actor GetActorById([Service]ApiDbContext context, int id) => context.Actors.Include(a => a.Roles).Include(a => a.Movies).FirstOrDefault(x => x.Id == id);

    /// <summary> Query for all producers in the database </summary>
    public List<Producer> GetProducers([Service]ApiDbContext context) => context.Producers.Include(p => p.Movies).ToList();

    /// <summary> Query for a specific producer in the database </summary>
    /// <param name="id"> The id of the producer </param>
    public Producer GetProducerById([Service]ApiDbContext context, int id) => context.Producers.Include(p => p.Movies).FirstOrDefault(x => x.Id == id);
}