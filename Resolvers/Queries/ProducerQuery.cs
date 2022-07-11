using Api.Models;
using Api.Models.DAO;

namespace Api.Resolvers.Queries;

/// <summary>
/// Class containing all possible query actions with producers
/// </summary>
[ExtendObjectType(typeof(Query))]
public class ProducerQuery {

    /// <summary> Query for all producers in the database </summary>
    [UseFiltering] 
    public IQueryable<Producer> GetProducers([Service(ServiceKind.Synchronized)]ProducerRepository repo) => repo.GetProducers;

    /// <summary> Query for a specific producer in the database </summary>
    /// <param name="id"> The id of the producer </param>
    public IQueryable<Producer> GetProducerById([Service(ServiceKind.Synchronized)]ProducerRepository repo, int id) => repo.GetProducerById(id);

}