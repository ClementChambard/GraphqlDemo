using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Models.DAO;

/// <summary>
/// Repository class for producers
/// </summary>
public class ProducerRepository {

    /// <summary> The context of the database </summary>
    private readonly ApiDbContext _context;

    /// <summary> Base constructor </summary>
    public ProducerRepository(ApiDbContext context)
    {
        this._context = context;
    }

    /// <summary> Query for all producers in the database </summary>
    public List<Producer> GetProducers => _context.Producers.Include(p => p.Movies).ToList();

    /// <summary> Query for a specific producer in the database </summary>
    /// <param name="id"> The id of the producer </param>
    public Producer GetProducerById(int id) => _context.Producers.Include(p => p.Movies).FirstOrDefault(x => x.Id == id);

    /// <summary> Mutation to add an producer to the database </summary>
    /// <param name="firstName"> The firstname of the producer </param>
    /// <param name="lastName"> The lastname of the producer </param>
    public async Task<Producer> AddProducer(string firstName, string lastName)
    {
        Producer producer = new Producer{FirstName = firstName, LastName = lastName};
        _context.Producers.Add(producer);
        await _context.SaveChangesAsync();
        return producer;
    }

    /// <summary> Mutation to remove an producer from the database </summary>
    /// <param name="producerId"> The id of the producer to remove </param>
    public async Task<Producer> RemoveProducer(int producerId)
    {
        Producer producer = _context.Producers.FirstOrDefault(x => x.Id == producerId);
        if (producer == null) return null;
        _context.Producers.Remove(producer);
        await _context.SaveChangesAsync();
        return producer;
    }

}