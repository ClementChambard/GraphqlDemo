using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Data.Models;

public class Role {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int MovieId { get; set; }
    public int ActorId { get; set; }
    public string Name { get; set; }

    public virtual Movie movie { get; set; }
    public virtual Actor actor { get; set; }
}
