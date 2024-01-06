using LibrarySPSTApi.Entities;
using LibrarySPSTApi.Interfaces;
using LibrarySPSTApi.Data;
using Microsoft.EntityFrameworkCore;

namespace LibrarySPSTApi.Services
{
    public class BookService(DataContext dbContext) : IBookService
    {
        private readonly DataContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task<IEnumerable<Book?>> GetAllBooksAsync()
        {
            return await _dbContext.Books.Include(book => book.Category).ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _dbContext.Books.Include(book => book.Category).FirstOrDefaultAsync(book => book.Id == id);
        }

        public async Task<Book?> AddBookAsync(Book book)
        {
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book> UpdateBookAsync(int id, Book book)
        {
            var existingBook = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (existingBook == null)
            {
                throw new InvalidOperationException("Book not found");
            }

            existingBook.Name = book.Name;
            existingBook.Description = book.Description;
            existingBook.AuthorName = book.AuthorName;
            existingBook.Pages = book.Pages;
            existingBook.Status = book.Status;
            existingBook.Year = book.Year;
            existingBook.Image = book.Image;
            existingBook.Quantity = book.Quantity;
            await _dbContext.SaveChangesAsync();
            return existingBook;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var bookToDelete = await _dbContext.Books.FirstOrDefaultAsync(book => book.Id == id);

            if (bookToDelete == null)
            {
                return false;
            }

            var category = await _dbContext
                .Categories.Include(c => c.Books)
                .FirstOrDefaultAsync(c => c.Id == bookToDelete.CategoryId);

            _dbContext.Books.Remove(bookToDelete);

            if (category != null && category.Books.Count == 1 && category.Books.Contains(bookToDelete))
            {
                _dbContext.Categories.Remove(category);
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}