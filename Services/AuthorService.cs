using LibrarySPSTApi.Entities;
using LibrarySPSTApi.Data;
using Microsoft.EntityFrameworkCore;

namespace LibrarySPSTApi.Services;

public class AuthorService(DataContext context)
{
    public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await context.Authors.ToListAsync();
        }
    
        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return (await context.Authors.FirstOrDefaultAsync(a => a.Id == id))!;
        }
    
        public async Task<Author> CreateAuthorAsync(Author newAuthor)
        {
            context.Authors.Add(newAuthor);
            await context.SaveChangesAsync();
            return newAuthor;
        }
    
        public async Task<Author> UpdateAuthorAsync(int id, Author updatedAuthor)
        {
            var existingAuthor = await context.Authors.FirstOrDefaultAsync(a => a.Id == id);

            if (existingAuthor == null) return existingAuthor!;
            existingAuthor.Name = updatedAuthor.Name;
            existingAuthor.AuthorDescription = updatedAuthor.AuthorDescription;
            existingAuthor.LitPeriod = updatedAuthor.LitPeriod;
            existingAuthor.Birth = updatedAuthor.Birth;
            existingAuthor.Death = updatedAuthor.Death;
            existingAuthor.AuthorImage = updatedAuthor.AuthorImage;
    
            await context.SaveChangesAsync();

            return existingAuthor!;
        }
    
        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var authorToDelete = await context.Authors.FirstOrDefaultAsync(a => a.Id == id);

            if (authorToDelete == null) return false;
            context.Authors.Remove(authorToDelete);
            await context.SaveChangesAsync();
            return true;

        }
}