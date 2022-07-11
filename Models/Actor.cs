using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

/// <summary>
/// Dataclass for an actor
/// </summary>
public class Actor {
    
    /// <summary> The unique ID of the actor </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }

    /// <summary> The firstname of the actor </summary>
    public string FirstName { get; set; }

    /// <summary> The lastname of the actor </summary>
    public string LastName { get; set; }

    /// <summary> The list of movies the actor played in </summary>
    [UseFiltering] 
    public virtual ICollection<Movie> Movies { get; set; }

    /// <summary> The list of roles the actor played </summary>
    [UseFiltering] 
    public virtual ICollection<Role> Roles { get; set; }

}