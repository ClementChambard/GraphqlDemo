using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Models.DAO;

/// <summary>
/// Repository class for movies
/// </summary>
public class MovieRepository {

    /// <summary> The context of the database </summary>
    private readonly ApiDbContext _context;

    /// <summary> Base constructor </summary>
    public MovieRepository(ApiDbContext context)
    {
        this._context = context;
    }

    /// <summary> Query for all movies in the database </summary>
    public List<Movie> GetMovies => _context.Movies.Include(m => m.Actors).Include(m => m.MovieProducer).Include(m => m.Roles).ToList();

    /// <summary> Query for a specific movie in the database </summary>
    /// <param name="id"> The id of the movie </param>
    public Movie GetMovieById(int id) => _context.Movies.Include(m => m.Actors).Include(m => m.MovieProducer).Include(m => m.Roles).FirstOrDefault(x => x.Id == id);

    /// <summary> Mutation to add a movie to the database </summary>
    /// <param name="producerId"> The id of the producer of the movie </param>
    /// <param name="title"> The title of the movie </param>
    /// <param name="actors"> The actors that played in the movie </param>
    public async Task<Movie> AddMovie(string title, int? producerId, List<Actor> actors)
    {
        Movie movie = new Movie{Title = title, Actors = actors, MovieProducer = _context.Producers.FirstOrDefault(x => x.Id == producerId)};
        if (actors == null) 
        {
            movie.Actors = new List<Actor>();
        }
        else 
        {
            foreach (Actor a in actors) 
            {
                if (_context.Actors.FirstOrDefault(x => ((x.FirstName == a.FirstName) && (x.LastName == a.LastName)) || (x.Id == a.Id)) == null)
                    _context.Actors.Add(a);
            }
        }
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();
        return movie;
    }

    /// <summary> Mutation to remove a movie from the database </summary>
    /// <param name="movieId"> The id of the movie to remove </param>
    public async Task<Movie> RemoveMovie(int movieId)
    {
        Movie movie = _context.Movies.FirstOrDefault(x => x.Id == movieId);
        if (movie == null) return null;
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
        return movie;
    }

    /// <summary> Mutation to add an actor to a movie </summary>
    /// <param name="movieId"> The id of the movie to add the actor to </param>
    /// <param name="actorId"> The id of the actor to add to the movie </param>
    public async Task<Movie> AddActorToMovie(int actorId, int movieId)
    {
        Actor actor = _context.Actors.FirstOrDefault(x => x.Id == actorId);
        Movie movie = _context.Movies.Include(m => m.Actors).FirstOrDefault(x => x.Id == movieId);
        if ((movie == null) || (actor == null)) return null; // find out how to send error message here
        movie.Actors.Add(actor);
        await _context.SaveChangesAsync();
        return movie;
    }

    /// <summary> Mutation to remove an actor from a movie </summary>
    /// <param name="movieId"> The id of the movie to remove the actor from </param>
    /// <param name="actorId"> The id of the actor to remove from the movie </param>
    public async Task<Actor> RemoveActorFromMovie(int movieId, int actorId)
    {
        Movie movie = _context.Movies.Include(m => m.Actors).FirstOrDefault(x => x.Id == movieId);
        if (movie == null) return null;
        Actor actor = movie.Actors.FirstOrDefault(x => x.Id == actorId);
        if (actor == null) return null;
        movie.Actors.Remove(actor);
        await _context.SaveChangesAsync();
        return actor;
    }

    /// <summary> Mutation to change a movie's producer </summary>
    /// <param name="movieId"> The id of the movie to update </param>
    /// <param name="producerId"> The id of the producer to associate with the movie </param>
    public async Task<Movie> ChangeMovieProducer(int movieId, int producerId)
    {
        Movie movie = _context.Movies.Include(m => m.MovieProducer).FirstOrDefault(x => x.Id == movieId);
        if (movie == null) return null;
        Producer producer = _context.Producers.FirstOrDefault(x => x.Id == producerId);
        if (producer == null) return null;
        movie.MovieProducer = producer;
        await _context.SaveChangesAsync();
        return movie;
    }

}