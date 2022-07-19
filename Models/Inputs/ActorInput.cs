namespace Api.Models.Inputs;

/// <summary>
/// Input class for the addActor mutation 
/// </summary>
public class AddActorInput {

    /// <summary> The firstname of the new actor </summary>
    public string FirstName { get; set; }

    /// <summary> The lastname of the new actor </summary>
    public string LastName { get; set; }

}

/// <summary>
/// Input class for the removeActor mutation 
/// </summary>
public class RemoveActorInput {

    /// <summary> The id of the actor to delete</summary>
    public int ActorId { get; set; }

}