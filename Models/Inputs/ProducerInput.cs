namespace Api.Models.Inputs;

/// <summary>
/// Input class for the addProducer mutation 
/// </summary>
public class AddProducerInput {

    /// <summary> The firstname of the new producer </summary>
    public string FirstName { get; set; }

    /// <summary> The lastname of the new producer </summary>
    public string LastName { get; set; }

}

/// <summary>
/// Input class for the removeProducer mutation 
/// </summary>
public class RemoveProducerInput {

    /// <summary> The id of the producer to delete</summary>
    public int ProducerId { get; set; }

}