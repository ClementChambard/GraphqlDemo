using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;


/// <summary>
/// Dataclass for a movie
/// </summary>
public class Movie {

    /// <summary> The unique ID of the movie </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary> The unique ID of the producer of the movie </summary>
    public int ProducerId { get; set; }

    /// <summary> The title of the movie </summary>
    public string Title { get; set; }

    /// <summary> The producer of the movie </summary>
    [UseFiltering] 
    public virtual Producer MovieProducer { get; set; }

    /// <summary> The list of actors that played in the movie </summary>
    [UseFiltering] 
    public virtual ICollection<Actor> Actors { get; set; }

    /// <summary> The list of roles of the movie </summary>
    [UseFiltering] 
    public virtual ICollection<Role> Roles { get; set; }

}