using CRUDproject.Contracts;
using CRUDproject.Domain.Interfaces;
using CRUDproject.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDproject.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController (IBookService booksService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<BooksResponse>>> GetBooks()
    {
        var books = await booksService.GetAllBooks();
        
        var response = books.Select(b => new BooksResponse(b.Id, b.Title, b.Author, b.Description, b.Price)).ToList();
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateBook([FromBody] BooksRequest request)
    {
        try
        {
            var book = new Book(
                Guid.NewGuid(),
                request.Title,
                request.Author,
                request.Description,
                request.Price
            );
            
            return  Ok(await booksService.CreateBook(book));
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateBook(Guid id, [FromBody] BooksRequest request)
    {
        try
        {
            return Ok(await booksService.UpdateBook(new Book(id, request.Title, request.Author, request.Description, request.Price)));
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);

        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteBook(Guid id)
    {
        var result = await booksService.DeleteBook(id);
        
        return (result != 0) ? Ok(id) : BadRequest("Book not found");
    }
}