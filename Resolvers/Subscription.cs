using HotChocolate.Execution;
using HotChocolate.Subscriptions;

using Api.Data.Models;

namespace Api.Resolvers;

public class Subscription {

    [Topic]
    [Subscribe]
    public Actor OnNewActor([EventMessage]Actor actor)
    {
        return actor;
    }
    
}