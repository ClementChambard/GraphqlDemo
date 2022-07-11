using Api.Models;
using Api.Models.DAO;

namespace Api.Resolvers.Mutations;

/// <summary>
/// Class containing all possible mutation actions for producers
/// </summary>
[ExtendObjectType(typeof(Mutation))]
public class ProducerMutation {

    /// <summary> Mutation to add a producer to the database </summary>
    /// <param name="firstName"> The firstname of the producer </param>
    /// <param name="lastName"> The lastname of the producer </param>
    public async Task<Producer> NewProducer([Service(ServiceKind.Synchronized)]ProducerRepository repo, string firstName, string lastName)
            => await repo.AddProducer(firstName, lastName);

    /// <summary> Mutation to remove a producer from the database </summary>
    /// <param name="producerId"> The id of the producer to remove </param>
    public async Task<Producer> RemoveProducer([Service(ServiceKind.Synchronized)]ProducerRepository repo, int producerId)
            => await repo.RemoveProducer(producerId);

}