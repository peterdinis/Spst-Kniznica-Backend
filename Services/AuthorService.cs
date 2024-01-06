using LibrarySPSTApi.Data;
using LibrarySPSTApi.Entities;
using LibrarySPSTApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibrarySPSTApi.Services;

public class AuthorService(DataContext dbContext) : IAuthorService
{
    public async Task<IEnumerable<Author?>> GetAllAuthorsAsync()
    {
        return await dbContext.Authors.ToListAsync();
    }

    public async Task<Author?> GetAuthorByIdAsync(int id)
    {
        return await dbContext.Authors.FirstOrDefaultAsync(author => author!.Id == id)!
            ?? throw new InvalidOperationException("Author not found");
    }

    public async Task<Author?> CreateAuthorAsync(Author? author)
    {
        dbContext.Authors.Add(author!);
        await dbContext.SaveChangesAsync();
        return author;
    }

    public async Task<bool> DeleteAuthorAsync(int id)
    {
        var authorToDelete = await dbContext.Authors.FirstOrDefaultAsync(author => author.Id == id);

        if (authorToDelete == null)
            return false;
        dbContext.Authors.Remove(authorToDelete);
        await dbContext.SaveChangesAsync();
        return true;
    }
}
