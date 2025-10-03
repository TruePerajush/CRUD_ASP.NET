using CRUDproject.Database.Entities;
using CRUDproject.Domain.Interfaces;
using CRUDproject.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDproject.Database.Repositories;

public class BookRepository(BookStoreDbContext context) : IBookRepository
{
    public async Task<List<Book>> GetAll()
    {
        var booksEntities = await context.Books
            .AsNoTracking()
            .ToListAsync();
        
        var books = booksEntities
            .Select(b => new Book(b.Id, b.Title, b.Author, b.Description, b.Price))
            .ToList();
        
        return books;
    }

    public async Task<Book> GetById(Guid id)
    {
        var book = await context.Books
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);
        
        if (book == null)
            throw new ArgumentException("Book not found");
        
        return new Book(book.Id, book.Title, book.Author, book.Description, book.Price);
    }

    public async Task<Guid> Create(Book book)
    {
        var bookEntity = new BookEntity
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Description = book.Description,
            Price = book.Price,
        };
        
        await context.Books.AddAsync(bookEntity);
        await context.SaveChangesAsync();
        
        return bookEntity.Id;
    }

    public async Task<Guid> Update(Book book)
    {
        await context.Books
            .Where(b => b.Id == book.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.Title, book.Title)
                .SetProperty(b => b.Author, book.Author)
                .SetProperty(b => b.Description, book.Description)
                .SetProperty(b => b.Price, book.Price)
            );
        
        return book.Id;
    }

    public async Task<int> Delete(Guid id)
    {
        return await context.Books
            .Where(b => b.Id == id)
            .ExecuteDeleteAsync();
    }
}