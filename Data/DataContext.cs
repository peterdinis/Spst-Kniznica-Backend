using Microsoft.EntityFrameworkCore;
using LibrarySPSTApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LibrarySPSTApi.data;

public class DataContext: IdentityDbContext<ApplicationUser>
{
    public DataContext(DbContextOptions<DataContext> options): base(options) {}
    
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<Teacher> Teachers { get; set; }
    
    public DbSet<Student> Students { get; set; }
    
    public DbSet<Admin> Admins { get; set; }
    
    
}