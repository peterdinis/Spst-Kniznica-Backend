using LibrarySPSTApi.Entities;
using LibrarySPSTApi.Data;
using Microsoft.EntityFrameworkCore;

namespace LibrarySPSTApi.Services;

public class AuthorService
{
    private readonly DataContext _context;
    
        public AuthorService(DataContext context)
        {
            _context = context;
        }
    
        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }
    
        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        }
    
        public async Task<Author> CreateAuthorAsync(Author newAuthor)
        {
            _context.Authors.Add(newAuthor);
            await _context.SaveChangesAsync();
            return newAuthor;
        }
    
        public async Task<Author> UpdateAuthorAsync(int id, Author updatedAuthor)
        {
            var existingAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
    
            if (existingAuthor != null)
            {
                existingAuthor.Name = updatedAuthor.Name;
                existingAuthor.AuthorDescription = updatedAuthor.AuthorDescription;
                existingAuthor.LitPeriod = updatedAuthor.LitPeriod;
                existingAuthor.Birth = updatedAuthor.Birth;
                existingAuthor.Death = updatedAuthor.Death;
                existingAuthor.AuthorImage = updatedAuthor.AuthorImage;
    
                await _context.SaveChangesAsync();
            }
    
            return existingAuthor;
        }
    
        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var authorToDelete = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
    
            if (authorToDelete != null)
            {
                _context.Authors.Remove(authorToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
    
            return false;
        }
}