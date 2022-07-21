using MinimalApi.Domain;

namespace MinimalApi.Repository;

public interface IBookRepository
{
    Task Insert(Book entry);
    Task<Book?> GetById(int id);
    Task<List<Book>> GetAll();
    void Delete(Book entry);
    Task SaveChangeAsync();
}