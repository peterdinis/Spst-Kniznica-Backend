using LibrarySPSTApi.Entities;

namespace LibrarySPSTApi.Interfaces
{
    public interface IAuthorService {
        Task<IEnumerable<Author?>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorByIdAsync(int id);
        Task<Author?> AddAuthorAsync(Author author);
        Task<bool> DeleteAuthorAsync(int id);
    }
}