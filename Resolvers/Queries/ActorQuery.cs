using Api.Models;
using Api.Models.DAO;

namespace Api.Resolvers.Queries;

/// <summary>
/// Class containing all possible query actions with actors
/// </summary>
[ExtendObjectType(typeof(Query))]
public class ActorQuery {

    /// <summary> Query for all actors in the database </summary>
    public List<Actor> GetActors([Service]ActorRepository repo) => repo.GetActors;

    /// <summary> Query for a specific actor in the database </summary>
    /// <param name="id"> The id of the actor </param>
    public Actor GetActorById([Service]ActorRepository repo, int id) => repo.GetActorById(id);

}