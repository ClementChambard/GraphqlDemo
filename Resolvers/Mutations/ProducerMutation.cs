using Api.Models;
using Api.Models.Inputs;
using Api.Models.Payloads;
using Api.Models.DAO;

namespace Api.Resolvers.Mutations;

/// <summary>
/// Class containing all possible mutation actions for producers
/// </summary>
[ExtendObjectType(typeof(Mutation))]
public class ProducerMutation {

    /// <summary> Mutation to add a producer to the database </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<AddProducerPayload> AddProducer([Service(ServiceKind.Synchronized)]ProducerRepository repo, AddProducerInput input)
            => await repo.AddProducer(input);

    /// <summary> Mutation to remove a producer from the database </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<RemoveProducerPayload> RemoveProducer([Service(ServiceKind.Synchronized)]ProducerRepository repo, RemoveProducerInput input)
            => await repo.RemoveProducer(input);

}