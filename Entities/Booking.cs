using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySPSTApi.Entities;

[Table(("bookings"))]
public class Booking
{
    [Key]
    public int Id { get; set; }

    public string BookName { get; set; } = null!;
    
    public DateTime From { get; set; }
    
    public DateTime To { get; set; }
}
