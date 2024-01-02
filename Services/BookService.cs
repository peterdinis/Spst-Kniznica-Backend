using LibrarySPSTApi.data;
using Microsoft.EntityFrameworkCore;
using LibrarySPSTApi.Entities;
using LibrarySPSTApi.Interfaces;

namespace LibrarySPSTApi.Services
{
    public class BookService : IBookService
    {
        private readonly DataContext _dbContext;

        public BookService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Book?>> GetAllBooksAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(book => book!.Id == id);
        }

        public async Task<Book?> AddBookAsync(Book? book)
        {
            _dbContext.Books.Add(book!);
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book> UpdateBookAsync(int id, Book book)
        {
            var existingBook = await _dbContext.Books.FirstOrDefaultAsync(b => b!.Id == id);

            if (existingBook == null) return existingBook!;
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
            var bookToDelete = await _dbContext.Books.FirstOrDefaultAsync(book => book!.Id == id);

            if (bookToDelete == null) return false;
            _dbContext.Books.Remove(bookToDelete);
            await _dbContext.SaveChangesAsync();
            return true;

        }
    }
}