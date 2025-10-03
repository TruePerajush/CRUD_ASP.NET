using CRUDproject.Domain.Models;

namespace CRUDproject.Domain.Interfaces;

public interface IBookRepository
{
    Task<List<Book>> GetAll();
    Task<Book> GetById(Guid id);
    Task<Guid> Create(Book book);
    Task<Guid> Update(Book book);
    Task<int> Delete(Guid id);
}