using System.ComponentModel.DataAnnotations;

namespace CRUDproject.Contracts;

public record BooksRequest(
    [Required] string Title,
    [Required] string Author,
    [Required] string Description,
    [Required] decimal Price);