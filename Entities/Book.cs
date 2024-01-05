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

    public string Pages { get; set; } = null!;

    public string Status { get; set; } = null!;
    
    public DateTime Year { get; set; }

    public string Image { get; set; } = null!;

    public string Quantity { get; set; } = null!;

    public string Publisher { get; set; } = null!;
    
    public int CategoryId { get; set; }

    // Navigation property
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }

    public int AuthorId { get; set; }
    
    public Author Author { get; set; }

}