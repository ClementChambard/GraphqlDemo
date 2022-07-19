namespace Api.Models.Payloads;

/// <summary>
/// Payload class for the addMovie mutation 
/// </summary>
public class AddMoviePayload {

    /// <summary> The movie created by the mutation addMovie </summary>
    public Movie CreatedMovie{ get; set; }

}

/// <summary>
/// Payload class for the removeMovie mutation 
/// </summary>
public class RemoveMoviePayload {

    /// <summary> The movie deleted by the mutation removeMovie </summary>
    public Movie DeletedMovie { get; set; }

}

/// <summary>
/// Payload class for the addActorToMovie mutation 
/// </summary>
public class AddActorToMoviePayload {

    /// <summary> The movie updated by the mutation </summary>
    public Movie UpdatedMovie { get; set; }

    /// <summary> The actor added by the mutation </summary>
    public Actor AddedActor { get; set; }

}

/// <summary>
/// Payload class for the removeActorFromMovie mutation 
/// </summary>
public class RemoveActorFromMoviePayload {

    /// <summary> The movie updated by the mutation </summary>
    public Movie UpdatedMovie { get; set; }

    /// <summary> The actor removed by the mutation </summary>
    public Actor RemovedActor { get; set; }

}

/// <summary>
/// Payload class for the changeMovieProducer mutation 
/// </summary>
public class ChangeMovieProducerPayload {

    /// <summary> The movie updated by the mutation </summary>
    public Movie UpdatedMovie { get; set; }

    /// <summary> The old producer of the movie </summary>
    public Producer OldProducer { get; set; }

    /// <summary> The new producer of the movie </summary>
    public Producer NewProducer { get; set; }

}