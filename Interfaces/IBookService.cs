using LibrarySPSTApi.Entities;

namespace LibrarySPSTApi.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book?>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book?> AddBookAsync(Book book);
        Task<Book> UpdateBookAsync(int id, Book book);
        Task<bool> DeleteBookAsync(int id);
    }
}
