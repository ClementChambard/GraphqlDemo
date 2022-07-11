using Api.Data;
using Api.Models;
using Api.Models.DAO;
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
    public async Task<Actor> NewActor([Service]ActorRepository repo, [Service]ITopicEventSender sender, string firstName, string lastName) 
            => await repo.AddActor(sender, firstName, lastName);

    /// <summary> Mutation to remove an actor from the database </summary>
    /// <param name="actorId"> The id of the actor to remove </param>
    public async Task<Actor> RemoveActor([Service]ActorRepository repo, int actorId)
            => await repo.RemoveActor(actorId);

    /// <summary> Mutation to add a movie to the database </summary>
    /// <param name="producerId"> The id of the producer of the movie </param>
    /// <param name="title"> The title of the movie </param>
    /// <param name="actors"> The actors that played in the movie </param>
    public async Task<Movie> NewMovie([Service]MovieRepository repo, int? producerId, string title, List<Actor> actors)
            => await repo.AddMovie(title, producerId, actors);

    /// <summary> Mutation to add an actor to a movie </summary>
    /// <param name="movieId"> The id of the movie to add the actor to </param>
    /// <param name="actorId"> The id of the actor to add to the movie </param>
    public async Task<Movie> AddActorToMovie([Service]MovieRepository repo, int movieId, int actorId)
            => await repo.AddActorToMovie(actorId, movieId);

    /// <summary> Mutation to remove a movie from the database </summary>
    /// <param name="movieId"> The id of the movie to remove </param>
    public async Task<Movie> RemoveMovie([Service]MovieRepository repo, int movieId)
            => await repo.RemoveMovie(movieId);

    /// <summary> Mutation to remove an actor from a movie </summary>
    /// <param name="movieId"> The id of the movie to remove the actor from </param>
    /// <param name="actorId"> The id of the actor to remove from the movie </param>
    public async Task<Actor> RemoveActorFromMovie([Service]MovieRepository repo, int movieId, int actorId)
            => await repo.RemoveActorFromMovie(movieId, actorId);

    /// <summary> Mutation to change a movie's producer </summary>
    /// <param name="movieId"> The id of the movie to update </param>
    /// <param name="producerId"> The id of the producer to associate with the movie </param>
    public async Task<Movie> ChangeMovieProducer([Service]MovieRepository repo, int movieId, int producerId)
            => await repo.ChangeMovieProducer(movieId, producerId);

    /// <summary> Mutation to add a role to the database </summary>
    /// <param name="name"> The name of the role </param>
    /// <param name="movieId"> The id of the movie of the role </param>
    /// <param name="actorId"> The id of the actor playing the role </param>
    public async Task<Role> NewRole([Service]RoleRepository repo, string name, int? movieId, int? actorId)
            => await repo.AddRole(name, actorId, movieId);

    /// <summary> Mutation to remove a role from the database </summary>
    /// <param name="roleId"> The id of the role to remove </param>
    public async Task<Role> RemoveRole([Service]RoleRepository repo, int roleId)
            => await repo.RemoveRole(roleId);

    /// <summary> Mutation to change a role's actor </summary>
    /// <param name="roleId"> The id of the role to update </param>
    /// <param name="actorId"> The id of the actor to associate with the role </param>
    public async Task<Actor> ChangeRoleActor([Service]RoleRepository repo, int roleId, int actorId)
            => await repo.ChangeRoleActor(roleId, actorId);

    /// <summary> Mutation to change a role's movie </summary>
    /// <param name="roleId"> The id of the role to update </param>
    /// <param name="movieId"> The id of the movie to associate with the role </param>
    public async Task<Movie> ChangeRoleMovie([Service]RoleRepository repo, int roleId, int movieId)
            => await repo.ChangeRoleMovie(roleId, movieId);

    /// <summary> Mutation to add a producer to the database </summary>
    /// <param name="firstName"> The firstname of the producer </param>
    /// <param name="lastName"> The lastname of the producer </param>
    public async Task<Producer> NewProducer([Service]ProducerRepository repo, string firstName, string lastName)
            => await repo.AddProducer(firstName, lastName);

    /// <summary> Mutation to remove a producer from the database </summary>
    /// <param name="producerId"> The id of the producer to remove </param>
    public async Task<Producer> RemoveProducer([Service]ProducerRepository repo, int producerId)
            => await repo.RemoveProducer(producerId);

}