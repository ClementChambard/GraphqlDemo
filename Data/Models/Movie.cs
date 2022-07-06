using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Data.Models;

public class Movie {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int ProducerId { get; set; }
    public string Title { get; set; }


    public virtual Producer producer { get; set; }
    public virtual ICollection<Actor> Actors { get; set; }
    public virtual ICollection<Role> Roles { get; set; }

}