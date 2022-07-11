using Api.Resolvers;
using Api.Data;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace Api.Models.DAO;

/// <summary>
/// Repository class for actors
/// </summary>
public class ActorRepository {

    /// <summary> The context of the database </summary>
    private readonly ApiDbContext _context;

    /// <summary> Base constructor </summary>
    public ActorRepository(ApiDbContext context)
    {
        this._context = context;
    }

    /// <summary> Query for all actors in the database </summary>
    public List<Actor> GetActors => _context.Actors.Include(a => a.Roles).Include(a => a.Movies).ToList();

    /// <summary> Query for a specific actor in the database </summary>
    /// <param name="id"> The id of the actor </param>
    public Actor GetActorById(int id) => _context.Actors.Include(a => a.Roles).Include(a => a.Movies).FirstOrDefault(x => x.Id == id);

    /// <summary> Mutation to add an actor to the database </summary>
    /// <param name="firstName"> The firstname of the actor </param>
    /// <param name="lastName"> The lastname of the actor </param>
    public async Task<Actor> AddActor([Service]ITopicEventSender sender, string firstName, string lastName)
    {
        Actor actor = new Actor{FirstName = firstName, LastName = lastName};
        _context.Actors.Add(actor);
        await _context.SaveChangesAsync();
        await sender.SendAsync(nameof(Subscription.OnNewActor), actor);
        return actor;
    }

    /// <summary> Mutation to remove an actor from the database </summary>
    /// <param name="actorId"> The id of the actor to remove </param>
    public async Task<Actor> RemoveActor(int actorId)
    {
        Actor actor = _context.Actors.FirstOrDefault(x => x.Id == actorId);
        if (actor == null) return null;
        _context.Actors.Remove(actor);
        await _context.SaveChangesAsync();
        return actor;
    }

}