using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace LibrarySPSTApi.Entities;

[Table("authors")]
public class Author
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string AuthorDescription { get; set; } = null!;

    public string AuthorImage { get; set; } = null!;

    public string LitPeriod { get; set; } = null!;

    public DateTime Birth { get; set; }

    public DateTime? Death { get; set; }

    public ICollection<Book> Books { get; set; }
}
