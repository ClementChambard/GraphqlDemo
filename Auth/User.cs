using System.ComponentModel.DataAnnotations;

namespace Api.Auth;

/// <summary>
/// Dataclass for users
/// </summary>
public class User {

    /// <summary> The unique id of the user </summary>
    [Key]
    public int UserId { get; set; }

    /// <summary> The firstname of the user </summary>
    public string FirstName { get; set; }

    /// <summary> The lastname of the user </summary>
    public string LastName { get; set; }

    /// <summary> The email address of the user </summary>
    public string EmailAddress { get; set; }

    /// <summary> A hash of the password of the user </summary>
    public string Password { get; set; }

    /// <summary> UNUSED </summary>
    public string RefreshToken { get; set; }

    /// <summary> UNUSED </summary>
    public string RefershTokenExpiration { get; set; }
}