using LibrarySPSTApi.Data;
using Microsoft.EntityFrameworkCore;
using LibrarySPSTApi.Entities;
using LibrarySPSTApi.Interfaces;

namespace LibrarySPSTApi.Services;

public class AuthorService(DataContext dbContext): IAuthorService
{
    public async Task<IEnumerable<Author?>> GetAllAuthorsAsync()
    {
        return await dbContext.Authors.ToListAsync();
    }
    
    public async Task<Author?> GetAuthorByIdAsync(int id)
    {
        return await dbContext.Authors.FirstOrDefaultAsync(author => author!.Id == id)! ?? throw new InvalidOperationException("Author not found");
    }
    
    public async Task<Author?> CreateAuthorAsync(Author? author)
    {
        dbContext.Authors.Add(author!);
        await dbContext.SaveChangesAsync();
        return author;
    }
}