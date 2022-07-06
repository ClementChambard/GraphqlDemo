using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Data.Models;

public class Role {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public Movie movie { get; set; }
    public Actor actor { get; set; }

    public string movieTitle => movie.Title;
    public string actorName => actor.fullname();
}
