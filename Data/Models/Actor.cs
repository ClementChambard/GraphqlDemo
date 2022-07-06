using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Data.Models;

public class Actor {
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string fullname() => (FirstName == null || LastName == null) ? null : FirstName + " " + LastName;

}