using Api.Models.Inputs;
using Api.Models.Payloads;
using Api.Models;
using Api.Models.DAO;

namespace Api.Resolvers.Mutations;

/// <summary>
/// Class containing all possible mutation actions for movies
/// </summary>
[ExtendObjectType(typeof(Mutation))]
public class MovieMutation {

    /// <summary> Mutation to add a movie to the database </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<AddMoviePayload> AddMovie([Service(ServiceKind.Synchronized)]MovieRepository repo, AddMovieInput input)
            => await repo.AddMovie(input);

    /// <summary> Mutation to remove a movie from the database </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<RemoveMoviePayload> RemoveMovie([Service(ServiceKind.Synchronized)]MovieRepository repo, RemoveMovieInput input)
            => await repo.RemoveMovie(input);

    /// <summary> Mutation to add an actor to a movie </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<AddActorToMoviePayload> AddActorToMovie([Service(ServiceKind.Synchronized)]MovieRepository repo, AddActorToMovieInput input)
            => await repo.AddActorToMovie(input);

    /// <summary> Mutation to remove an actor from a movie </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<RemoveActorFromMoviePayload> RemoveActorFromMovie([Service(ServiceKind.Synchronized)]MovieRepository repo, RemoveActorFromMovieInput input)
            => await repo.RemoveActorFromMovie(input);

    /// <summary> Mutation to change a movie's producer </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<ChangeMovieProducerPayload> ChangeMovieProducer([Service(ServiceKind.Synchronized)]MovieRepository repo, ChangeMovieProducerInput input)
            => await repo.ChangeMovieProducer(input);

}