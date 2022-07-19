using Api.Models;
using Api.Models.Inputs;
using Api.Models.Payloads;
using Api.Models.DAO;

namespace Api.Resolvers.Mutations;

/// <summary>
/// Class containing all possible mutation actions for roles
/// </summary>
[ExtendObjectType(typeof(Mutation))]
public class RoleMutation {

    /// <summary> Mutation to add a role to the database </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<AddRolePayload> AddRole([Service(ServiceKind.Synchronized)]RoleRepository repo, AddRoleInput input)
            => await repo.AddRole(input);

    /// <summary> Mutation to remove a role from the database </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<RemoveRolePayload> RemoveRole([Service(ServiceKind.Synchronized)]RoleRepository repo, RemoveRoleInput input)
            => await repo.RemoveRole(input);

    /// <summary> Mutation to change a role's actor </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<ChangeRoleActorPayload> ChangeRoleActor([Service(ServiceKind.Synchronized)]RoleRepository repo, ChangeRoleActorInput input)
            => await repo.ChangeRoleActor(input);

    /// <summary> Mutation to change a role's movie </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<ChangeRoleMoviePayload> ChangeRoleMovie([Service(ServiceKind.Synchronized)]RoleRepository repo, ChangeRoleMovieInput input)
            => await repo.ChangeRoleMovie(input);

}