using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Data.Models;

public class Movie {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Title { get; set; }

    public Producer producer { get; set; }

    public List<Actor> Actors { get; set; }

    public List<string> actorsNames() { List<string> names = new List<string>(); foreach (Actor a in Actors) names.Add(a.fullname()); return names; }
    public string producerName() => producer.fullname();

}