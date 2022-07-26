namespace Api.Models.Payloads;

/// <summary>
/// Payload class for the addActor mutation 
/// </summary>
public class AddActorPayload {

    /// <summary> The actor created by the mutation addActor </summary>
    public Actor CreatedActor { get; set; }

}

/// <summary>
/// Payload class for the removeActor mutation 
/// </summary>
public class RemoveActorPayload {

    /// <summary> The actor deleted by the mutation removeActor </summary>
    public Actor DeletedActor { get; set; }

    /// <summary> The status of the mutation removeActor </summary>
    public String StatusString { get; set; }

}