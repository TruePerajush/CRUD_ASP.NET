using CRUDproject.Domain.Interfaces;
using CRUDproject.Domain.Models;

namespace CRUDproject.Application.Services;

public class BookService (IBookRepository repository) : IBookService
{
    public async Task<List<Book>> GetAllBooks()
    {
        return await repository.Get();
    }

    public async Task<Guid> CreateBook(Book book)
    {
        return await repository.Create(book);
    }

    public async Task<Guid> UpdateBook(Book book)
    {
        return await repository.Update(book);
    }

    public async Task<int> DeleteBook(Guid id)
    {
        return await repository.Delete(id);
    }
}