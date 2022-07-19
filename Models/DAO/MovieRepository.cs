using Api.Data;
using Api.Models.Inputs;
using Api.Models.Payloads;
using Microsoft.EntityFrameworkCore;

namespace Api.Models.DAO;

/// <summary>
/// Repository class for movies
/// </summary>
public class MovieRepository {

    /// <summary> The context of the database </summary>
    private readonly ApiDbContext _context;

    /// <summary> Base constructor </summary>
    public MovieRepository([Service(ServiceKind.Synchronized)]ApiDbContext context)
    {
        this._context = context;
    }

    /// <summary> Query for all movies in the database </summary>
    public IQueryable<Movie> GetMovies => _context.Movies.Include(m => m.Actors).Include(m => m.MovieProducer).Include(m => m.Roles);

    /// <summary> Query for a specific movie in the database </summary>
    /// <param name="id"> The id of the movie </param>
    public IQueryable<Movie> GetMovieById(int? id) => _context.Movies.Include(m => m.Actors).Include(m => m.MovieProducer).Include(m => m.Roles).Where(x => x.Id == id);

    /// <summary> Mutation to add a movie to the database </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<AddMoviePayload> AddMovie(AddMovieInput input)
    {
        Movie movie = new Movie{Title = input.Title, Actors = input.Actors, MovieProducer = _context.Producers.FirstOrDefault(x => x.Id == input.ProducerId)};
        if (input.Actors is null) 
        {
            movie.Actors = new List<Actor>();
        }
        else 
        {
            foreach (Actor a in input.Actors) 
            {
                if (_context.Actors.FirstOrDefault(x => ((x.FirstName == a.FirstName) && (x.LastName == a.LastName)) || (x.Id == a.Id)) == null)
                    _context.Actors.Add(a);
            }
        }
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();
        return new AddMoviePayload{CreatedMovie = movie};
    }

    /// <summary> Mutation to remove a movie from the database </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<RemoveMoviePayload> RemoveMovie(RemoveMovieInput input)
    {
        Movie movie = _context.Movies.FirstOrDefault(x => x.Id == input.MovieId);
        if (movie is null) return new RemoveMoviePayload{DeletedMovie = null};
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
        return new RemoveMoviePayload{DeletedMovie = movie};
    }

    /// <summary> Mutation to add an actor to a movie </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<AddActorToMoviePayload> AddActorToMovie(AddActorToMovieInput input)
    {
        Movie movie = _context.Movies.Include(m => m.Actors).FirstOrDefault(x => x.Id == input.MovieId);
        Actor actor = _context.Actors.FirstOrDefault(x => x.Id == input.ActorId);
        if (movie is null) return new AddActorToMoviePayload{UpdatedMovie = movie, AddedActor = actor};
        if (actor is not null) movie.Actors.Add(actor);
        await _context.SaveChangesAsync();
        return new AddActorToMoviePayload{UpdatedMovie = movie, AddedActor = actor};
    }

    /// <summary> Mutation to remove an actor from a movie </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<RemoveActorFromMoviePayload> RemoveActorFromMovie(RemoveActorFromMovieInput input)
    {
        Movie movie = _context.Movies.Include(m => m.Actors).FirstOrDefault(x => x.Id == input.MovieId);
        Actor actor = movie.Actors.FirstOrDefault(x => x.Id == input.ActorId);
        if (movie is null) return new RemoveActorFromMoviePayload{UpdatedMovie = movie, RemovedActor = actor};
        if (actor is not null) movie.Actors.Remove(actor);
        await _context.SaveChangesAsync();
        return new RemoveActorFromMoviePayload{UpdatedMovie = movie, RemovedActor = actor};
    }

    /// <summary> Mutation to change a movie's producer </summary>
    /// <param name="input"> Input for the mutation </param>
    public async Task<ChangeMovieProducerPayload> ChangeMovieProducer(ChangeMovieProducerInput input)
    {
        Movie movie = _context.Movies.Include(m => m.MovieProducer).FirstOrDefault(x => x.Id == input.MovieId);
        Producer producer = _context.Producers.FirstOrDefault(x => x.Id == input.ProducerId);
        if (movie is null) return new ChangeMovieProducerPayload{UpdatedMovie = movie, NewProducer = producer, OldProducer = null};
        Producer old = movie.MovieProducer;
        movie.MovieProducer = producer;
        await _context.SaveChangesAsync();
        return new ChangeMovieProducerPayload{UpdatedMovie = movie, NewProducer = producer, OldProducer = old};
    }

}