using Api.Models;
using Api.Models.DAO;

namespace Api.Resolvers.Queries;

/// <summary>
/// Class containing all possible query actions with roles
/// </summary>
[ExtendObjectType(typeof(Query))]
public class RoleQuery {

    /// <summary> Query for all roles in the database </summary>
    public List<Role> GetRoles([Service]RoleRepository repo) => repo.GetRoles;

    /// <summary> Query for a specific role in the database </summary>
    /// <param name="id"> The id of the role </param>
    public Role GetRoleById([Service]RoleRepository repo, int id) => repo.GetRoleById(id);

}