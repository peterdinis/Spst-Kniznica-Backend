using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySPSTApi.Entities;

[Table(("publishers"))]
public class Publisher
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }
}
