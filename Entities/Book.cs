using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySPSTApi.Entities;

[Table("books")]
public class Book
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string AuthorName { get; set; } = null!;
    
    public int Pages { get; set; }

    public string Status { get; set; } = null!;
    
    public DateTime Year { get; set; }

    public string Image { get; set; } = null!;

    public int Quantity { get; set; }

}