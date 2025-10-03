using System.Text.Json;
using CRUDproject.Domain.Interfaces;
using CRUDproject.Domain.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace CRUDproject.Application.Services;

public class BookService: IBookService
{
    private readonly IBookRepository _repository;
    private readonly IDistributedCache _cache;
    private readonly DistributedCacheEntryOptions _options = new DistributedCacheEntryOptions();

    public BookService(IBookRepository repository, IDistributedCache cache)
    {
        _repository = repository;
        _cache = cache;
        
        _options.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
        _options.SetSlidingExpiration(TimeSpan.FromMinutes(1));
    }
    
    public async Task<List<Book>> GetAllBooks()
    {   
        return await _repository.GetAll();
    }

    public async Task<Book> GetBookById(Guid id)
    {
        var cacheJsonGetResult = await _cache.GetStringAsync(id.ToString());
        if (cacheJsonGetResult == null)
            return await _repository.GetById(id);
        
        var book = JsonSerializer.Deserialize<Book>(cacheJsonGetResult)!;
        
        return book;
    }

    public async Task<Guid> CreateBook(Book book)
    {   
        var checkIfExists = await _cache.GetStringAsync(book.Id.ToString());
        if (checkIfExists != null)
            throw new ArgumentException("Such key already exists!");
        
        await _cache.SetStringAsync(book.Id.ToString(), JsonSerializer.Serialize(book));
        return await _repository.Create(book);
    }

    public async Task<Guid> UpdateBook(Book book)
    {
        var repositoryGuid = await _repository.Update(book);
        await _cache.SetStringAsync(book.Id.ToString(), JsonSerializer.Serialize(book));
        return repositoryGuid;
    }

    public async Task<int> DeleteBook(Guid id)
    {
        await _cache.RemoveAsync(id.ToString());
        return await _repository.Delete(id);
    }
}