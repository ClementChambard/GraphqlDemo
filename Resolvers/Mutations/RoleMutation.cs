using Api.Models;
using Api.Models.DAO;

namespace Api.Resolvers.Mutations;

/// <summary>
/// Class containing all possible mutation actions for roles
/// </summary>
[ExtendObjectType(typeof(Mutation))]
public class RoleMutation {

    /// <summary> Mutation to add a role to the database </summary>
    /// <param name="name"> The name of the role </param>
    /// <param name="movieId"> The id of the movie of the role </param>
    /// <param name="actorId"> The id of the actor playing the role </param>
    public async Task<Role> NewRole([Service]RoleRepository repo, string name, int? movieId, int? actorId)
            => await repo.AddRole(name, actorId, movieId);

    /// <summary> Mutation to remove a role from the database </summary>
    /// <param name="roleId"> The id of the role to remove </param>
    public async Task<Role> RemoveRole([Service]RoleRepository repo, int roleId)
            => await repo.RemoveRole(roleId);

    /// <summary> Mutation to change a role's actor </summary>
    /// <param name="roleId"> The id of the role to update </param>
    /// <param name="actorId"> The id of the actor to associate with the role </param>
    public async Task<Actor> ChangeRoleActor([Service]RoleRepository repo, int roleId, int actorId)
            => await repo.ChangeRoleActor(roleId, actorId);

    /// <summary> Mutation to change a role's movie </summary>
    /// <param name="roleId"> The id of the role to update </param>
    /// <param name="movieId"> The id of the movie to associate with the role </param>
    public async Task<Movie> ChangeRoleMovie([Service]RoleRepository repo, int roleId, int movieId)
            => await repo.ChangeRoleMovie(roleId, movieId);

}