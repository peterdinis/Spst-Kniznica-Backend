using LibrarySPSTApi.Data;
using LibrarySPSTApi.Entities;
using LibrarySPSTApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibrarySPSTApi.Services
{
    public class BookService(DataContext dbContext) : IBookService
    {
        public async Task<IEnumerable<Book?>> GetAllBooksAsync()
        {
            return await dbContext.Books.Include(book => book.Category).ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await dbContext.Books.FirstOrDefaultAsync(book => book!.Id == id)!
                ?? throw new InvalidOperationException("Book not found");
        }

        public async Task<Book?> AddBookAsync(Book? book)
        {
            // Check if the category associated with the book exists
            var categoryExists = await dbContext.Categories.AnyAsync(
                c => book != null && c.Id == book.CategoryId
            );

            if (!categoryExists)
            {
                return null;
            }

            dbContext.Books.Add(book!);
            await dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book> UpdateBookAsync(int id, Book book)
        {
            var existingBook = await dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (existingBook == null)
                return existingBook!;

            // Update properties other than Id
            existingBook.Name = book.Name;
            existingBook.Description = book.Description;
            existingBook.AuthorName = book.AuthorName;
            existingBook.Pages = book.Pages;
            existingBook.Status = book.Status;
            existingBook.Year = book.Year;
            existingBook.Image = book.Image;
            existingBook.Quantity = book.Quantity;

            await dbContext.SaveChangesAsync();

            return existingBook;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var bookToDelete = await dbContext.Books.FirstOrDefaultAsync(book => book.Id == id);

            if (bookToDelete == null)
            {
                return false;
            }

            var category = await dbContext
                .Categories.Include(category => category.Books)
                .FirstOrDefaultAsync(c => c.Id == bookToDelete.CategoryId);

            dbContext.Books.Remove(bookToDelete);

            // Check if this book was the only book in the category
            if (category is { Books.Count: 1 } && category.Books.Contains(bookToDelete))
            {
                dbContext.Categories.Remove(category);
            }

            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
