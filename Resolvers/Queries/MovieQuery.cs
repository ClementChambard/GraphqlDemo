using Api.Models;
using Api.Models.DAO;

namespace Api.Resolvers.Queries;

/// <summary>
/// Class containing all possible query actions with movies
/// </summary>
[ExtendObjectType(typeof(Query))]
public class MovieQuery {

    /// <summary> Query for all movies in the database </summary>
    public IQueryable<Movie> GetMovies([Service(ServiceKind.Synchronized)]MovieRepository repo) => repo.GetMovies;

    /// <summary> Query for a specific movie in the database </summary>
    /// <param name="id"> The id of the movie </param>
    public IQueryable<Movie> GetMovieById([Service(ServiceKind.Synchronized)]MovieRepository repo, int id) => repo.GetMovieById(id);

}