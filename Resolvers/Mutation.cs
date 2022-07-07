using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using HotChocolate.Subscriptions;

namespace Api.Resolvers;

/// <summary>
/// Class containing all possible mutation actions
/// </summary>
/// TODO: Separate actions for each entity in it's own separate class
public class Mutation {

    /// <summary> Mutation to add an actor to the database </summary>
    /// <param name="firstName"> The firstname of the actor </param>
    /// <param name="lastName"> The lastname of the actor </param>
    public async Task<Actor> NewActor([Service]ApiDbContext context, [Service]ITopicEventSender sender, string firstName, string lastName)
    {
        Actor actor = new Actor{FirstName = firstName, LastName = lastName};
        context.Actors.Add(actor);
        await context.SaveChangesAsync();
        await sender.SendAsync(nameof(Subscription.OnNewActor), actor);
        return actor;
    }

    /// <summary> Mutation to add a movie to the database </summary>
    /// <param name="producerId"> The id of the producer of the movie </param>
    /// <param name="title"> The title of the movie </param>
    /// <param name="actors"> The actors that played in the movie </param>
    public async Task<Movie> NewMovie([Service]ApiDbContext context, int? producerId, string title, List<Actor> actors)
    {
        Movie movie = new Movie{Title = title, Actors = actors, MovieProducer = context.Producers.FirstOrDefault(x => x.Id == producerId)};
        if (actors == null) 
        {
            movie.Actors = new List<Actor>();
        }
        else 
        {
            foreach (Actor a in actors) 
            {
                if (context.Actors.FirstOrDefault(x => ((x.FirstName == a.FirstName) && (x.LastName == a.LastName)) || (x.Id == a.Id)) == null)
                    context.Actors.Add(a);
            }
        }
        context.Movies.Add(movie);
        await context.SaveChangesAsync();
        return movie;
    }

    /// <summary> Mutation to add an actor to a movie </summary>
    /// <param name="movieId"> The id of the movie to add the actor to </param>
    /// <param name="actorId"> The id of the actor to add to the movie </param>
    public async Task<Movie> AddActorToMovie([Service]ApiDbContext context, int movieId, int actorId)
    {
        Actor actor = context.Actors.FirstOrDefault(x => x.Id == actorId);
        Movie movie = context.Movies.Include(m => m.Actors).FirstOrDefault(x => x.Id == movieId);
        if ((movie == null) || (actor == null)) return null; // find out how to send error message here
        movie.Actors.Add(actor);
        await context.SaveChangesAsync();
        return movie;
    }

    /// <summary> Mutation to remove a movie from the database </summary>
    /// <param name="movieId"> The id of the movie to remove </param>
    public async Task<Movie> RemoveMovie([Service]ApiDbContext context, int movieId)
    {
        Movie movie = context.Movies.FirstOrDefault(x => x.Id == movieId);
        if (movie == null) return null; 
        context.Movies.Remove(movie);
        await context.SaveChangesAsync();
        return movie;
    }

    /// <summary> Mutation to remove an actor from the database </summary>
    /// <param name="actorId"> The id of the actor to remove </param>
    public async Task<Actor> RemoveActor([Service]ApiDbContext context, int actorId)
    {
        Actor actor = context.Actors.FirstOrDefault(x => x.Id == actorId);
        if (actor == null) return null;
        context.Actors.Remove(actor);
        await context.SaveChangesAsync();
        return actor;
    }

    /// <summary> Mutation to remove an actor from a movie </summary>
    /// <param name="movieId"> The id of the movie to remove the actor from </param>
    /// <param name="actorId"> The id of the actor to remove from the movie </param>
    public async Task<Actor> RemoveActorFromMovie([Service]ApiDbContext context, int movieId, int actorId)
    {
        Movie movie = context.Movies.Include(m => m.Actors).FirstOrDefault(x => x.Id == movieId);
        if (movie == null) return null;
        Actor actor = movie.Actors.FirstOrDefault(x => x.Id == actorId);
        if (actor == null) return null;
        movie.Actors.Remove(actor);
        await context.SaveChangesAsync();
        return actor;
    }

    /// <summary> Mutation to add a role to the database </summary>
    /// <param name="name"> The name of the role </param>
    /// <param name="movieId"> The id of the movie of the role </param>
    /// <param name="actorId"> The id of the actor playing the role </param>
    public async Task<Role> NewRole([Service]ApiDbContext context, string name, int? movieId, int? actorId)
    {
        Movie movie = context.Movies.FirstOrDefault(x => x.Id == movieId);
        Actor actor = context.Actors.FirstOrDefault(x => x.Id == actorId);
        Role role = new Role{Name = name, RoleActor = actor, RoleMovie = movie};
        context.Roles.Add(role);
        await context.SaveChangesAsync();
        return role;
    }

    /// <summary> Mutation to remove a role from the database </summary>
    /// <param name="actorId"> The id of the role to remove </param>
    public async Task<Role> RemoveRole([Service]ApiDbContext context, int roleId)
    {
        Role role = context.Roles.FirstOrDefault(x => x.Id == roleId);
        if (role == null) return null;
        context.Roles.Remove(role);
        await context.SaveChangesAsync();
        return role;
    }

    /// <summary> Mutation to change a role's actor </summary>
    /// <param name="roleId"> The id of the role to update </param>
    /// <param name="actorId"> The id of the actor to associate with the role </param>
    public async Task<Actor> ChangeRoleActor([Service]ApiDbContext context, int roleId, int actorId)
    {
        Role role = context.Roles.Include(x => x.RoleActor).FirstOrDefault(x => x.Id == roleId);
        if (role == null) return null;
        Actor actor = context.Actors.FirstOrDefault(x => x.Id == actorId);
        if (actor == null) return null;
        role.RoleActor = actor;
        await context.SaveChangesAsync();
        return actor;
    }

    /// <summary> Mutation to change a role's movie </summary>
    /// <param name="roleId"> The id of the role to update </param>
    /// <param name="movieId"> The id of the movie to associate with the role </param>
    public async Task<Movie> ChangeRoleMovie([Service]ApiDbContext context, int roleId, int movieId)
    {
        Role role = context.Roles.Include(x => x.RoleMovie).FirstOrDefault(x => x.Id == roleId);
        if (role == null) return null;
        Movie movie = context.Movies.FirstOrDefault(x => x.Id == movieId);
        if (movie == null) return null;
        role.RoleMovie = movie;
        await context.SaveChangesAsync();
        return movie; 
    }

    /// <summary> Mutation to add a producer to the database </summary>
    /// <param name="firstName"> The firstname of the producer </param>
    /// <param name="lastName"> The lastname of the producer </param>
    public async Task<Producer> NewProducer([Service]ApiDbContext context, string firstName, string lastName)
    {
        Producer producer = new Producer{FirstName = firstName, LastName = lastName};
        context.Producers.Add(producer);
        await context.SaveChangesAsync();
        return producer;
    }

    /// <summary> Mutation to remove a producer from the database </summary>
    /// <param name="actorId"> The id of the producer to remove </param>
    public async Task<Producer> RemoveProducer([Service]ApiDbContext context, int producerId)
    {
        Producer producer = context.Producers.FirstOrDefault(x => x.Id == producerId);
        if (producer == null) return null;
        context.Producers.Remove(producer);
        await context.SaveChangesAsync();
        return producer;
    }

    /// <summary> Mutation to change a movie's producer </summary>
    /// <param name="movieId"> The id of the movie to update </param>
    /// <param name="producerId"> The id of the producer to associate with the movie </param>
    public async Task<Movie> ChangeMovieProducer([Service]ApiDbContext context, int movieId, int producerId)
    {
        Movie movie = context.Movies.Include(m => m.MovieProducer).FirstOrDefault(x => x.Id == movieId);
        if (movie == null) return null;
        Producer producer = context.Producers.FirstOrDefault(x => x.Id == producerId);
        if (producer == null) return null;
        movie.MovieProducer = producer;
        await context.SaveChangesAsync();
        return movie;
    }

}