using Api.Data;
using Api.Models.Inputs;
using Api.Models.Payloads;
using Microsoft.EntityFrameworkCore;

namespace Api.Models.DAO;

/// <summary>
/// Repository class for producers
/// </summary>
public class ProducerRepository {

    /// <summary> The context of the database </summary>
    private readonly ApiDbContext _context;

    /// <summary> Base constructor </summary>
    public ProducerRepository([Service(ServiceKind.Synchronized)]ApiDbContext context)
    {
        this._context = context;
    }

    /// <summary> Query for all producers in the database </summary>
    public IQueryable<Producer> GetProducers => _context.Producers.Include(p => p.Movies);

    /// <summary> Query for a specific producer in the database </summary>
    /// <param name="id"> The id of the producer </param>
    public IQueryable<Producer> GetProducerById(int id) => _context.Producers.Include(p => p.Movies).Where(x => x.Id == id);

    /// <summary> Mutation to add an producer to the database </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<AddProducerPayload> AddProducer(AddProducerInput input)
    {
        Producer producer = new Producer{FirstName = input.FirstName, LastName = input.LastName};
        _context.Producers.Add(producer);
        await _context.SaveChangesAsync();
        return new AddProducerPayload{CreatedProducer = producer};
    }

    /// <summary> Mutation to remove an producer from the database </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<RemoveProducerPayload> RemoveProducer(RemoveProducerInput input)
    {
        Producer producer = _context.Producers.FirstOrDefault(x => x.Id == input.ProducerId);
        if (producer is null) return new RemoveProducerPayload{DeletedProducer = null};
        _context.Producers.Remove(producer);
        await _context.SaveChangesAsync();
        return new RemoveProducerPayload{DeletedProducer = producer};
    }

}