using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

/// <summary>
/// Dataclass for a producer
/// </summary>
public class Producer {
    
    /// <summary> The unique ID of the producer </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary> The firstname of the producer </summary>
    public string FirstName { get; set; }

    /// <summary> The lastname of the producer </summary>
    public string LastName { get; set; }

    /// <summary> The list of movies the producer produced </summary>
    public virtual ICollection<Movie> Movies { get; set; }

}