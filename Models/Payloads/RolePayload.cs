namespace Api.Models.Payloads;

/// <summary>
/// Payload class for the addRole mutation 
/// </summary>
public class AddRolePayload {

    /// <summary> The role created by the mutation addRole </summary>
    public Role CreatedRole{ get; set; }

}

/// <summary>
/// Payload class for the removeRole mutation 
/// </summary>
public class RemoveRolePayload {

    /// <summary> The role deleted by the mutation removeRole </summary>
    public Role DeletedRole { get; set; }

}

/// <summary>
/// Payload class for the changeRoleActor mutation 
/// </summary>
public class ChangeRoleActorPayload {

    /// <summary> The role updated by the mutation </summary>
    public Role UpdatedRole { get; set; }

    /// <summary> The old actor of the role </summary>
    public Actor OldActor { get; set; }

    /// <summary> The new actor of the role </summary>
    public Actor NewActor { get; set; }

}

/// <summary>
/// Payload class for the changeRoleMovie mutation 
/// </summary>
public class ChangeRoleMoviePayload {

    /// <summary> The role updated by the mutation </summary>
    public Role UpdatedRole { get; set; }

    /// <summary> The old movie of the role </summary>
    public Movie OldMovie { get; set; }

    /// <summary> The new movie of the role </summary>
    public Movie NewMovie { get; set; }

}