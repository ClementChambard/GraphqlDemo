using Api.Resolvers;
using Api.Data;
using Api.Models.Inputs;
using Api.Models.Payloads;
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
    public ActorRepository([Service(ServiceKind.Synchronized)]ApiDbContext context)
    {
        this._context = context;
    }

    /// <summary> Query for all actors in the database </summary>
    public IQueryable<Actor> GetActors => _context.Actors.Include(a => a.Roles).Include(a => a.Movies);

    /// <summary> Query for a specific actor in the database </summary>
    /// <param name="id"> The id of the actor </param>
    public IQueryable<Actor> GetActorById(int? id) => _context.Actors.Include(a => a.Roles).Include(a => a.Movies).Where(x => x.Id == id);

    /// <summary> Mutation to add an actor to the database </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<AddActorPayload> AddActor([Service]ITopicEventSender sender, AddActorInput input)
    {
        Actor actor = new Actor{FirstName = input.FirstName, LastName = input.LastName};
        _context.Actors.Add(actor);
        await _context.SaveChangesAsync();
        await sender.SendAsync(nameof(Subscription.OnNewActor), actor);
        return new AddActorPayload{CreatedActor = actor};
    }

    /// <summary> Mutation to remove an actor from the database </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<RemoveActorPayload> RemoveActor(RemoveActorInput input)
    {
        Actor actor = _context.Actors.FirstOrDefault(x => x.Id == input.ActorId);
        if (actor is null) return new RemoveActorPayload{DeletedActor = null};
        _context.Actors.Remove(actor);
        await _context.SaveChangesAsync();
        return new RemoveActorPayload{DeletedActor = actor};
    }

}