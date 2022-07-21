using Microsoft.EntityFrameworkCore;
using MinimalApi.Data;
using MinimalApi.Domain;

namespace MinimalApi.Repository;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _dbContext;

    public BookRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Insert(Book entry)
    {
        _dbContext.Books.AddAsync(entry);
        return Task.CompletedTask;
    }

    public Task<Book?> GetById(int id)
    {
        return _dbContext.Books.FirstOrDefaultAsync(s => s.Id == id);
    }

    public Task<List<Book>> GetAll()
    {
        return _dbContext.Books.ToListAsync();
    }

    public void Delete(Book entry)
    {
        _dbContext.Books.Remove(entry);
    }

    public async Task SaveChangeAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}