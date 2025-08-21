using CRUDproject.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUDproject.Database;

public class BookStoreDbContext : DbContext
{
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<BookEntity> Books { get; set; }
}