using Api.Models;
using Api.Models.DAO;

namespace Api.Resolvers.Queries;

/// <summary>
/// Class containing all possible query actions with producers
/// </summary>
[ExtendObjectType(typeof(Query))]
public class ProducerQuery {

    /// <summary> Query for all producers in the database </summary>
    public List<Producer> GetProducers([Service]ProducerRepository repo) => repo.GetProducers;

    /// <summary> Query for a specific producer in the database </summary>
    /// <param name="id"> The id of the producer </param>
    public Producer GetProducerById([Service]ProducerRepository repo, int id) => repo.GetProducerById(id);

}