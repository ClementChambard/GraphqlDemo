namespace Api.Models.Inputs;

/// <summary>
/// Input class for the addMovie mutation 
/// </summary>
public class AddMovieInput {

    /// <summary> The title of the new movie </summary>
    public string Title { get; set; }

    /// <summary> The id of the producer for the new movie </summary>
    public int? ProducerId { get; set; }

    /// <summary> The list of actors for the new movie </summary>
    public List<Actor> Actors { get; set; }

}

/// <summary>
/// Input class for the removeMovie mutation 
/// </summary>
public class RemoveMovieInput {

    /// <summary> The id of the movie to delete </summary>
    public int MovieId { get; set; }

}

/// <summary>
/// Input class for the addActorToMovie mutation 
/// </summary>
public class AddActorToMovieInput {

    /// <summary> The id of the movie to update </summary>
    public int MovieId { get; set; }

    /// <summary> The id of the actor to add </summary>
    public int ActorId { get; set; }

}

/// <summary>
/// Input class for the removeActorFromMovie mutation 
/// </summary>
public class RemoveActorFromMovieInput {

    /// <summary> The id of the movie to update </summary>
    public int MovieId { get; set; }

    /// <summary> The id of the actor to remove </summary>
    public int ActorId { get; set; }

}

/// <summary>
/// Input class for the changeMovieProducer mutation 
/// </summary>
public class ChangeMovieProducerInput {

    /// <summary> The id of the movie to update </summary>
    public int MovieId { get; set; }

    /// <summary> The id of the producer to set </summary>
    public int ProducerId { get; set; }

}