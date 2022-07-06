using Api.Data.Models;
using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Resolvers;

public class Query {

    public List<Movie> GetMovies([Service]ApiDbContext context) => context.Movies.Include(m => m.Actors).Include(m => m.producer).Include(m => m.Roles).ToList();

    public Movie GetMovieById([Service]ApiDbContext context, int id) => context.Movies.Include(m => m.Actors).Include(m => m.producer).Include(m => m.Roles).FirstOrDefault(x => x.Id == id);

    public List<Role> GetRoles([Service]ApiDbContext context) => context.Roles.Include(r => r.actor).Include(r => r.movie).ToList();

    public Role GetRoleById([Service]ApiDbContext context, int id) => context.Roles.Include(r => r.actor).Include(r => r.movie).FirstOrDefault(x => x.Id == id);

    public List<Actor> GetActors([Service]ApiDbContext context) => context.Actors.Include(a => a.Roles).Include(a => a.Movies).ToList();

    public Actor GetActorById([Service]ApiDbContext context, int id) => context.Actors.Include(a => a.Roles).Include(a => a.Movies).FirstOrDefault(x => x.Id == id);

    public List<Producer> GetProducers([Service]ApiDbContext context) => context.Producers.Include(p => p.Movies).ToList();

    public Producer GetProducerById([Service]ApiDbContext context, int id) => context.Producers.Include(p => p.Movies).FirstOrDefault(x => x.Id == id);
}