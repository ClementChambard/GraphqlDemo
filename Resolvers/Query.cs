using Api.Data.Models;
using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Resolvers;

public class Query {

    public List<Movie> GetMovies([Service]ApiDbContext context) => context.Movies.Include(m => m.Actors).Include(m => m.producer).ToList();

    public Movie GetMovieById([Service]ApiDbContext context, int id) => context.Movies.Include(m => m.Actors).Include(m => m.producer).FirstOrDefault(x => x.Id == id);

    public List<Role> GetRoles([Service]ApiDbContext context) => context.Roles.Include(m => m.actor).Include(m => m.movie).ToList();

    public Role GetRoleById([Service]ApiDbContext context, int id) => context.Roles.Include(m => m.actor).Include(m => m.movie).FirstOrDefault(x => x.Id == id);

    public List<Actor> GetActors([Service]ApiDbContext context) => context.Actors.ToList();

    public Actor GetActorById([Service]ApiDbContext context, int id) => context.Actors.FirstOrDefault(x => x.Id == id);

    public List<Producer> GetProducers([Service]ApiDbContext context) => context.Producers.ToList();

    public Producer GetProducerById([Service]ApiDbContext context, int id) => context.Producers.FirstOrDefault(x => x.Id == id);
}