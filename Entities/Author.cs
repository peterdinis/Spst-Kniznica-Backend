using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace LibrarySPSTApi.Entities;

[Table("authors")]
public class Author
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }
    
    public string AuthorDescription { get; set; }
    
    public string AuthorImage { get; set; }
    
    public string LitPeriod { get; set; }
    
    public DateTime Birth { get; set; }
    
    public DateTime? Death { get; set; }
    
    
}