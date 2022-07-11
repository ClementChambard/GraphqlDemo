
using Api.Models;
using Api.Models.DAO;

namespace Api.Resolvers.Mutations;

/// <summary>
/// Class containing all possible mutation actions for movies
/// </summary>
[ExtendObjectType(typeof(Mutation))]
public class MovieMutation {

    /// <summary> Mutation to add a movie to the database </summary>
    /// <param name="producerId"> The id of the producer of the movie </param>
    /// <param name="title"> The title of the movie </param>
    /// <param name="actors"> The actors that played in the movie </param>
    public async Task<Movie> NewMovie([Service(ServiceKind.Synchronized)]MovieRepository repo, int? producerId, string title, List<Actor> actors)
            => await repo.AddMovie(title, producerId, actors);

    /// <summary> Mutation to add an actor to a movie </summary>
    /// <param name="movieId"> The id of the movie to add the actor to </param>
    /// <param name="actorId"> The id of the actor to add to the movie </param>
    public async Task<Movie> AddActorToMovie([Service(ServiceKind.Synchronized)]MovieRepository repo, int movieId, int actorId)
            => await repo.AddActorToMovie(actorId, movieId);

    /// <summary> Mutation to remove a movie from the database </summary>
    /// <param name="movieId"> The id of the movie to remove </param>
    public async Task<Movie> RemoveMovie([Service(ServiceKind.Synchronized)]MovieRepository repo, int movieId)
            => await repo.RemoveMovie(movieId);

    /// <summary> Mutation to remove an actor from a movie </summary>
    /// <param name="movieId"> The id of the movie to remove the actor from </param>
    /// <param name="actorId"> The id of the actor to remove from the movie </param>
    public async Task<Actor> RemoveActorFromMovie([Service(ServiceKind.Synchronized)]MovieRepository repo, int movieId, int actorId)
            => await repo.RemoveActorFromMovie(movieId, actorId);

    /// <summary> Mutation to change a movie's producer </summary>
    /// <param name="movieId"> The id of the movie to update </param>
    /// <param name="producerId"> The id of the producer to associate with the movie </param>
    public async Task<Movie> ChangeMovieProducer([Service(ServiceKind.Synchronized)]MovieRepository repo, int movieId, int producerId)
            => await repo.ChangeMovieProducer(movieId, producerId);

}