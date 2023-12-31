using Microsoft.EntityFrameworkCore;
using LibrarySPSTApi.Entities;

namespace LibrarySPSTApi.data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions<DataContext> options): base(options) {}
    
    public DbSet<Category> Categories { get; set; }
}