using CRUDproject.Domain.Models;

namespace CRUDproject.Domain.Interfaces;

public interface IBookService
{
    Task<List<Book>> GetAllBooks();
    Task<Book> GetBookById(Guid id);
    Task<Guid> CreateBook(Book book);
    Task<Guid> UpdateBook(Book book);
    Task<int> DeleteBook(Guid id);
}