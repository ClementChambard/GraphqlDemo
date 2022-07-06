using Api.Data;
using Api.Data.Models;
using Microsoft.EntityFrameworkCore;
using HotChocolate.Subscriptions;

namespace Api.Resolvers;

public class Mutation {
    public async Task<Actor> NewActor([Service]ApiDbContext context, [Service]ITopicEventSender sender, string firstName, string lastName)
    {
        Actor actor = new Actor{FirstName = firstName, LastName = lastName};
        context.Actors.Add(actor);
        await context.SaveChangesAsync();
        await sender.SendAsync(nameof(Subscription.OnNewActor), actor);
        return actor;
    }

    public async Task<Movie> NewMovie([Service]ApiDbContext context, int? producerId, string title, List<Actor> actors)
    {
        Movie movie = new Movie{Title = title, Actors = actors, producer = context.Producers.FirstOrDefault(x => x.Id == producerId)};
        if (actors == null) movie.Actors = new List<Actor>();
        else {
            foreach (Actor a in actors) {
                if (context.Actors.FirstOrDefault(x => (x.FirstName == a.FirstName && x.LastName == a.LastName) || x.Id == a.Id) == null)
                    context.Actors.Add(a);
            }
        }
        context.Movies.Add(movie);
        await context.SaveChangesAsync();
        return movie;
    }

    public async Task<Movie> AddActorToMovie([Service]ApiDbContext context, int movieId, int actorId)
    {
        Actor actor = context.Actors.FirstOrDefault(x => x.Id == actorId);
        Movie movie = context.Movies.Include(m => m.Actors).FirstOrDefault(x => x.Id == movieId);
        if (movie == null) { return null; } // find out how to send error message here
        if (actor == null) { return null; }
        movie.Actors.Add(actor);
        await context.SaveChangesAsync();
        return movie;
    }

    public async Task<Movie> RemoveMovie([Service]ApiDbContext context, int movieId)
    {
        Movie movie = context.Movies.FirstOrDefault(x => x.Id == movieId);
        if (movie == null) { return null; } 
        context.Movies.Remove(movie);
        await context.SaveChangesAsync();
        return movie;
    }

    public async Task<Actor> RemoveActor([Service]ApiDbContext context, int actorId)
    {
        Actor actor = context.Actors.FirstOrDefault(x => x.Id == actorId);
        if (actor == null) return null;
        context.Actors.Remove(actor);
        await context.SaveChangesAsync();
        return actor;
    }

    public async Task<Actor> RemoveActorFromMovie([Service]ApiDbContext context, int movieId, int actorId)
    {
        Movie movie = context.Movies.Include(m => m.Actors).FirstOrDefault(x => x.Id == movieId);
        if (movie == null) { return null; }
        Actor actor = movie.Actors.FirstOrDefault(x => x.Id == actorId);
        if (actor == null) { return null; }
        movie.Actors.Remove(actor);
        await context.SaveChangesAsync();
        return actor;
    }

    public async Task<Role> NewRole([Service]ApiDbContext context, string name, int? movieId, int? actorId)
    {
        Movie movie = context.Movies.FirstOrDefault(x => x.Id == movieId);
        Actor actor = context.Actors.FirstOrDefault(x => x.Id == actorId);
        Role role = new Role{Name = name, actor = actor, movie = movie};
        context.Roles.Add(role);
        await context.SaveChangesAsync();
        return role;
    }

    public async Task<Role> RemoveRole([Service]ApiDbContext context, int roleId)
    {
        Role role = context.Roles.FirstOrDefault(x => x.Id == roleId);
        if (role == null) return null;
        context.Roles.Remove(role);
        await context.SaveChangesAsync();
        return role;
    }

    public async Task<Actor> ChangeRoleActor([Service]ApiDbContext context, int roleId, int actorId)
    {
        Role role = context.Roles.Include(x => x.actor).FirstOrDefault(x => x.Id == roleId);
        if (role == null) return null;
        Actor actor = context.Actors.FirstOrDefault(x => x.Id == actorId);
        if (actor == null) return null;
        role.actor = actor;
        await context.SaveChangesAsync();
        return actor;
    }

    public async Task<Movie> ChangeRoleMovie([Service]ApiDbContext context, int roleId, int movieId)
    {
        Role role = context.Roles.Include(x => x.movie).FirstOrDefault(x => x.Id == roleId);
        if (role == null) return null;
        Movie movie = context.Movies.FirstOrDefault(x => x.Id == movieId);
        if (movie == null) return null;
        role.movie = movie;
        await context.SaveChangesAsync();
        return movie; 
    }

    public async Task<Producer> NewProducer([Service]ApiDbContext context, string firstName, string lastName)
    {
        Producer producer = new Producer{FirstName = firstName, LastName = lastName};
        context.Producers.Add(producer);
        await context.SaveChangesAsync();
        return producer;
    }

    public async Task<Producer> RemoveProducer([Service]ApiDbContext context, int producerId)
    {
        Producer producer = context.Producers.FirstOrDefault(x => x.Id == producerId);
        if (producer == null) return null;
        context.Producers.Remove(producer);
        await context.SaveChangesAsync();
        return producer;
    }

    public async Task<Movie> ChangeMovieProducer([Service]ApiDbContext context, int movieId, int producerId)
    {
        Movie movie = context.Movies.Include(m => m.producer).FirstOrDefault(x => x.Id == movieId);
        if (movie == null) return null;
        Producer producer = context.Producers.FirstOrDefault(x => x.Id == producerId);
        if (producer == null) return null;
        movie.producer = producer;
        await context.SaveChangesAsync();
        return movie;
    }

}