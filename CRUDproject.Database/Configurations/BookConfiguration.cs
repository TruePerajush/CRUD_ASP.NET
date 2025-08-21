using CRUDproject.Database.Entities;
using CRUDproject.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUDproject.Database.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<BookEntity>
{
    public void Configure(EntityTypeBuilder<BookEntity> builder)
    {
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(Book.MAX_TITLE_LENGTH);

        builder.Property(b => b.Author)
            .IsRequired();
        
        builder.Property(b => b.Description)
            .IsRequired();
        
        builder.Property(b => b.Price)
            .IsRequired();
    }
}