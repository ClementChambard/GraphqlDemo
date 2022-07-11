using Api.Models;
using Api.Models.DAO;

namespace Api.Resolvers.Queries;

/// <summary>
/// Class containing all possible query actions with roles
/// </summary>
[ExtendObjectType(typeof(Query))]
public class RoleQuery {

    /// <summary> Query for all roles in the database </summary>
    [UseFiltering] 
    public IQueryable<Role> GetRoles([Service(ServiceKind.Synchronized)]RoleRepository repo) => repo.GetRoles;

    /// <summary> Query for a specific role in the database </summary>
    /// <param name="id"> The id of the role </param>
    public IQueryable<Role> GetRoleById([Service(ServiceKind.Synchronized)]RoleRepository repo, int id) => repo.GetRoleById(id);

}