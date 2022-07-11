using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Models.DAO;

/// <summary>
/// Repository class for roles
/// </summary>
public class RoleRepository {

    /// <summary> The context of the database </summary>
    private readonly ApiDbContext _context;

    /// <summary> Base constructor </summary>
    public RoleRepository(ApiDbContext context)
    {
        this._context = context;
    }

    /// <summary> Query for all roles in the database </summary>
    public List<Role> GetRoles => _context.Roles.Include(r => r.RoleActor).Include(r => r.RoleMovie).ToList();

    /// <summary> Query for a specific role in the database </summary>
    /// <param name="id"> The id of the role </param>
    public Role GetRoleById(int id) => _context.Roles.Include(r => r.RoleActor).Include(r => r.RoleMovie).FirstOrDefault(x => x.Id == id);

    /// <summary> Mutation to add an role to the database </summary>
    /// <param name="firstName"> The firstname of the role </param>
    /// <param name="lastName"> The lastname of the role </param>
    public async Task<Role> AddRole(string name, int? actorId, int? movieId)
    {
        Movie movie = _context.Movies.FirstOrDefault(x => x.Id == movieId);
        Actor actor = _context.Actors.FirstOrDefault(x => x.Id == actorId);
        Role role = new Role{Name = name, RoleActor = actor, RoleMovie = movie};
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        return role;
    }

    /// <summary> Mutation to remove an role from the database </summary>
    /// <param name="roleId"> The id of the role to remove </param>
    public async Task<Role> RemoveRole(int roleId)
    {
        Role role = _context.Roles.FirstOrDefault(x => x.Id == roleId);
        if (role == null) return null;
        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
        return role;
    }

    /// <summary> Mutation to change a role's actor </summary>
    /// <param name="roleId"> The id of the role to update </param>
    /// <param name="actorId"> The id of the actor to associate with the role </param>
    public async Task<Actor> ChangeRoleActor(int roleId, int actorId)
    {
        Role role = _context.Roles.Include(x => x.RoleActor).FirstOrDefault(x => x.Id == roleId);
        if (role == null) return null;
        Actor actor = _context.Actors.FirstOrDefault(x => x.Id == actorId);
        if (actor == null) return null;
        role.RoleActor = actor;
        await _context.SaveChangesAsync();
        return actor;
    }

    /// <summary> Mutation to change a role's movie </summary>
    /// <param name="roleId"> The id of the role to update </param>
    /// <param name="movieId"> The id of the movie to associate with the role </param>
    public async Task<Movie> ChangeRoleMovie(int roleId, int movieId)
    {
        Role role = _context.Roles.Include(x => x.RoleMovie).FirstOrDefault(x => x.Id == roleId);
        if (role == null) return null;
        Movie movie = _context.Movies.FirstOrDefault(x => x.Id == movieId);
        if (movie == null) return null;
        role.RoleMovie = movie;
        await _context.SaveChangesAsync();
        return movie; 
    }

}