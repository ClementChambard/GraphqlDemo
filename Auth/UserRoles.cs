using System.ComponentModel.DataAnnotations;

namespace Api.Auth;

/// <summary> </summary>
public class UserRoles {

    /// <summary> </summary>
    [Key]
    public int RoleId { get; set; }

    /// <summary> </summary>
    public int UserId { get; set; }

    /// <summary> </summary>
    public string Name { get; set; }

}