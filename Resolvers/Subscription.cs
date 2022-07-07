using Api.Models;

namespace Api.Resolvers;

/// <summary>
/// Class containing all possible subscription actions
/// </summary>
public class Subscription {

    /// <summary>
    /// Action to subscribe to actor creation
    /// </summary>
    /// <returns> The new actor </returns>
    [Topic]
    [Subscribe]
    public Actor OnNewActor([EventMessage]Actor actor)
    {
        return actor;
    }
    
}