namespace Api.Auth;

/// <summary>
/// Class for the jwt settings in appsettings.json
/// </summary>
public class TokenSettings {

    /// <summary> The token issuer </summary>
    public string Issuer { get; set; }

    /// <summary> The token audience </summary>
    public string Audience { get; set; }

    /// <summary> The key of the token </summary>
    public string Key { get; set; }
}