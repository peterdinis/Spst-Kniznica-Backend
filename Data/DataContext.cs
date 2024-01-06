using Microsoft.EntityFrameworkCore;
using LibrarySPSTApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LibrarySPSTApi.Data;

public class DataContext: IdentityDbContext<ApplicationUser>
{
    public DataContext(DbContextOptions<DataContext> options): base(options) {}
    
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<Teacher> Teachers { get; set; }
    
    public DbSet<Student> Students { get; set; }
    
    public DbSet<Admin> Admins { get; set; }
    
    public DbSet<Book> Books { get; set; }
    
    public DbSet<Author> Authors { get; set; }
    
    public DbSet<Booking> Bookings { get; set; }
    
    public DbSet<Publisher> Publishers { get; set; }
}