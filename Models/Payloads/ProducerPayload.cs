namespace Api.Models.Payloads;

/// <summary>
/// Payload class for the addProducer mutation 
/// </summary>
public class AddProducerPayload {

    /// <summary> The producer created by the mutation addProducer </summary>
    public Producer CreatedProducer { get; set; }

}

/// <summary>
/// Payload class for the removeProducer mutation 
/// </summary>
public class RemoveProducerPayload {

    /// <summary> The producer deleted by the mutation removeProducer </summary>
    public Producer DeletedProducer { get; set; }

}