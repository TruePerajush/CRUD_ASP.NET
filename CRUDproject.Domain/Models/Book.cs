namespace CRUDproject.Domain.Models;

public class Book
{
    public const int MAX_TITLE_LENGTH = 100;
    public const int MAX_AUTHOR_LENGTH = 50;
    public const int MAX_DESCRIPTION_LENGTH = 500;

    public Book(Guid id, string title, string author, string description, decimal price)
    {
        Id =  id;
        Title = title;
        Author = author;
        Description = description;
        Price = price;
    }
    
    public Guid Id { get; set; }
    
    private string _title = string.Empty;
    public string Title
    {
        get => _title;
        set
        {
            if (string.IsNullOrEmpty(value) || value.Length > MAX_TITLE_LENGTH)
            {
                throw new ArgumentException($"Title length must be between 1 and {MAX_TITLE_LENGTH} characters");
            }
            
            _title = value;
        }
    } 
    
    private string _author = string.Empty;
    public string Author
    {
        get => _author;
        set
        {
            if (string.IsNullOrEmpty(value) || value.Length > MAX_AUTHOR_LENGTH)
            {
                throw new ArgumentException($"Author length must be between 1 and {MAX_AUTHOR_LENGTH} characters");
            }
            
            _author = value;
        }
    } 
    
    private  string _description = string.Empty;
    public string Description
    {
        get => _description;
        set
        {
            if (string.IsNullOrEmpty(value) || value.Length > MAX_DESCRIPTION_LENGTH)
            {
                throw new ArgumentException($"Description length must be between 1 and {MAX_DESCRIPTION_LENGTH} characters");
            }
            
            _description = value;
        }
    }

    private decimal _price;
    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Price must be greater than or equal to 0");
            }
            
            _price = value;
        }
    }
}