using Api.Models;
using Api.Models.Payloads;
using Api.Models.Inputs;
using Api.Models.DAO;
using HotChocolate.Subscriptions;

namespace Api.Resolvers.Mutations;

/// <summary>
/// Class containing all possible mutation actions for actors
/// </summary>
[ExtendObjectType(typeof(Mutation))]
public class ActorMutation {

    /// <summary> Mutation to add an actor to the database </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<AddActorPayload> AddActor([Service(ServiceKind.Synchronized)]ActorRepository repo, [Service]ITopicEventSender sender, AddActorInput input) 
            => await repo.AddActor(sender, input);

    /// <summary> Mutation to remove an actor from the database </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<RemoveActorPayload> RemoveActor([Service(ServiceKind.Synchronized)]ActorRepository repo, RemoveActorInput input)
            => await repo.RemoveActor(input);

}