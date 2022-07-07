using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;


/// <summary>
/// Dataclass for a role
/// </summary>
public class Role {

    /// <summary> The unique ID of the role </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary> The unique ID of the role's movie </summary>
    public int MovieId { get; set; }

    /// <summary> The unique ID of the role's actor </summary>
    public int ActorId { get; set; }

    /// <summary> The name of the role </summary>
    public string Name { get; set; }

    /// <summary> The movie of the role </summary>
    public virtual Movie RoleMovie { get; set; }

    /// <summary> The actor playing the role </summary>
    public virtual Actor RoleActor { get; set; }
}
