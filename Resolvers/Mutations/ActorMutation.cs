using Api.Models;
using Api.Models.DAO;
using HotChocolate.Subscriptions;

namespace Api.Resolvers.Mutations;

/// <summary>
/// Class containing all possible mutation actions for actors
/// </summary>
[ExtendObjectType(typeof(Mutation))]
public class ActorMutation {

    /// <summary> Mutation to add an actor to the database </summary>
    /// <param name="firstName"> The firstname of the actor </param>
    /// <param name="lastName"> The lastname of the actor </param>
    public async Task<Actor> NewActor([Service(ServiceKind.Synchronized)]ActorRepository repo, [Service]ITopicEventSender sender, string firstName, string lastName) 
            => await repo.AddActor(sender, firstName, lastName);

    /// <summary> Mutation to remove an actor from the database </summary>
    /// <param name="actorId"> The id of the actor to remove </param>
    public async Task<Actor> RemoveActor([Service(ServiceKind.Synchronized)]ActorRepository repo, int actorId)
            => await repo.RemoveActor(actorId);

}