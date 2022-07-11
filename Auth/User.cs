using System.ComponentModel.DataAnnotations;

namespace Api.Auth;

/// <summary>
/// </summary>
public class User {

    /// <summary> </summary>
    [Key]
    public int UserId { get; set; }

    /// <summary> </summary>
    public string FirstName { get; set; }

    /// <summary> </summary>
    public string LastName { get; set; }

    /// <summary> </summary>
    public string EmailAddress { get; set; }

    /// <summary> </summary>
    public string Password { get; set; }

    /// <summary> </summary>
    public string RefreshToken { get; set; }

    /// <summary> </summary>
    public string RefershTokenExpiration { get; set; }
}