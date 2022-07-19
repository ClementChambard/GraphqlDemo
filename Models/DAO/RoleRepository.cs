using Api.Data;
using Api.Models.Inputs;
using Api.Models.Payloads;
using Microsoft.EntityFrameworkCore;

namespace Api.Models.DAO;

/// <summary>
/// Repository class for roles
/// </summary> 
public class RoleRepository {

    /// <summary> The context of the database </summary>
    private readonly ApiDbContext _context;

    /// <summary> Base constructor </summary>
    public RoleRepository([Service(ServiceKind.Synchronized)]ApiDbContext context)
    {
        this._context = context;
    }

    /// <summary> Query for all roles in the database </summary>
    public IQueryable<Role> GetRoles => _context.Roles.Include(r => r.RoleActor).Include(r => r.RoleMovie);

    /// <summary> Query for a specific role in the database </summary>
    /// <param name="id"> The id of the role </param>
    public IQueryable<Role> GetRoleById(int id) => _context.Roles.Include(r => r.RoleActor).Include(r => r.RoleMovie).Where(x => x.Id == id);

    /// <summary> Mutation to add an role to the database </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<AddRolePayload> AddRole(AddRoleInput input)
    {
        Movie movie = _context.Movies.FirstOrDefault(x => x.Id == input.MovieId);
        Actor actor = _context.Actors.FirstOrDefault(x => x.Id == input.ActorId);
        Role role = new Role{Name = input.Name, RoleActor = actor, RoleMovie = movie};
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        return new AddRolePayload{CreatedRole = role};
    }

    /// <summary> Mutation to remove an role from the database </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<RemoveRolePayload> RemoveRole(RemoveRoleInput input)
    {
        Role role = _context.Roles.FirstOrDefault(x => x.Id == input.RoleId);
        if (role is null) return new RemoveRolePayload{DeletedRole = null};
        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
        return new RemoveRolePayload{DeletedRole = role};
    }

    /// <summary> Mutation to change a role's actor </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<ChangeRoleActorPayload> ChangeRoleActor(ChangeRoleActorInput input)
    {
        Role role = _context.Roles.Include(x => x.RoleActor).FirstOrDefault(x => x.Id == input.RoleId);
        Actor actor = _context.Actors.FirstOrDefault(x => x.Id == input.ActorId);
        if (role is null) return new ChangeRoleActorPayload{UpdatedRole = null, NewActor = actor, OldActor = null};
        Actor old = role.RoleActor;
        role.RoleActor = actor;
        await _context.SaveChangesAsync();
        return new ChangeRoleActorPayload{UpdatedRole = role, NewActor = actor, OldActor = old};
    }

    /// <summary> Mutation to change a role's movie </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<ChangeRoleMoviePayload> ChangeRoleMovie(ChangeRoleMovieInput input)
    {
        Role role = _context.Roles.Include(x => x.RoleMovie).FirstOrDefault(x => x.Id == input.RoleId);
        Movie movie = _context.Movies.FirstOrDefault(x => x.Id == input.MovieId);
        if (role is null) return new ChangeRoleMoviePayload{UpdatedRole = null, NewMovie = movie, OldMovie = null};
        Movie old = role.RoleMovie;
        role.RoleMovie = movie;
        await _context.SaveChangesAsync();
        return new ChangeRoleMoviePayload{UpdatedRole = role, NewMovie = movie, OldMovie = old}; 
    }

}