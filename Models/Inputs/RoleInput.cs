namespace Api.Models.Inputs;

/// <summary>
/// Input class for the addRole mutation 
/// </summary>
public class AddRoleInput {

    /// <summary> The name of the new role </summary>
    public string Name { get; set; }

    /// <summary> The id of the actor for the new role </summary>
    public int? ActorId { get; set; }

    /// <summary> The id of the movie for the new role </summary>
    public int? MovieId { get; set; }

}

/// <summary>
/// Input class for the removeRole mutation 
/// </summary>
public class RemoveRoleInput {

    /// <summary> The id of the role to delete </summary>
    public int RoleId { get; set; }

}

/// <summary>
/// Input class for the changeRoleActor mutation 
/// </summary>
public class ChangeRoleActorInput {

    /// <summary> The id of the role to update </summary>
    public int RoleId { get; set; }

    /// <summary> The id of the actor to set </summary>
    public int ActorId { get; set; }

}

/// <summary>
/// Input class for the changeRoleMovie mutation 
/// </summary>
public class ChangeRoleMovieInput {

    /// <summary> The id of the role to update </summary>
    public int RoleId { get; set; }

    /// <summary> The id of the movie to set </summary>
    public int MovieId { get; set; }

}