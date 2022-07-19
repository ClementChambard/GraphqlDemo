namespace Api.Auth;

/// <summary>
/// Payload class for the register mutation 
/// </summary>
public class RegisterPayload {

    /// <summary> The status of the registration </summary>
    public string StatusString { get; set; }

}

/// <summary>
/// Payload class for the login mutation 
/// </summary>
public class LoginPayload {

    /// <summary> The access token </summary>
    public string Token { get; set; }

    /// <summary> The status of the connection </summary>
    public string StatusString { get; set; }

}