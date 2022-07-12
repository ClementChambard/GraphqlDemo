using System.ComponentModel.DataAnnotations;

namespace Api.Auth;

/// <summary> 
/// Dataclass for user roles
/// </summary>
public class UserRoles {

    /// <summary> The unique id of the user role </summary>
    [Key]
    public int RoleId { get; set; }

    /// <summary> The unique id of the user that has that role </summary>
    public int UserId { get; set; }

    /// <summary> The name of the role (e.g. admin, default) </summary>
    public string Name { get; set; }

}