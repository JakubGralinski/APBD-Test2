using APBD_Test2.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_Test2.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
    
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<BookGenre> BookGenres { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<PublishingHouse> PublishingHouses { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookGenre>().HasKey(bg => new { bg.IdGenre, bg.IdBook });
        modelBuilder.Entity<BookAuthor>().HasKey(ba => new { ba.IdBook, ba.IdAuthor });
        
        modelBuilder.Entity<BookGenre>()
            .HasOne(bg => bg.Genre)
            .WithMany(g => g.BookGenres)
            .HasForeignKey(bg => bg.IdGenre);
        
        modelBuilder.Entity<BookGenre>()
            .HasOne(bg => bg.Book)
            .WithMany(b => b.BookGenres)
            .HasForeignKey(bg => bg.IdBook);
        
        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Author)
            .WithMany(a => a.BookAuthors)
            .HasForeignKey(ba => ba.IdAuthor);
        
        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Book)
            .WithMany(b => b.BookAuthors)
            .HasForeignKey(ba => ba.IdBook);
        
        modelBuilder.Entity<Book>(e =>
        {
            e.ToTable("Book");
            e.HasKey(b => b.IdBook);
            e.Property(b => b.Name).IsRequired().HasMaxLength(50);
            e.Property(b => b.ReleaseDate).IsRequired();
            e.HasOne(b => b.PublishingHouse)
                .WithMany(ph => ph.Books)
                .HasForeignKey(b => b.IdPublishingHouse);
        });
        
        modelBuilder.Entity<Author>(e =>
            {
            e.ToTable("Author");
            e.HasKey(a => a.IdAuthor);
            e.Property(a => a.FirstName).IsRequired().HasMaxLength(50);
            e.Property(a => a.LastName).IsRequired().HasMaxLength(50);
        });
        
        modelBuilder.Entity<Genre>(e =>
        {
            e.ToTable("Genre");
            e.HasKey(g => g.IdGenre);
            e.Property(g => g.Name).IsRequired().HasMaxLength(50);
        });
        
        modelBuilder.Entity<PublishingHouse>(e =>
            {
            e.ToTable("PublishingHouse");
            e.HasKey(ph => ph.IdPublishingHouse);
            e.Property(ph => ph.Name).IsRequired().HasMaxLength(50);
            e.Property(ph => ph.Country).IsRequired().HasMaxLength(50);
            e.Property(ph => ph.City).IsRequired().HasMaxLength(50);
        });
        
        
        // Seed Data seed each table
        //modelBuilder.Entity<.HasData(
        //);
        
        modelBuilder.Entity<BookGenre>().HasData(
            new BookGenre { IdBook = 1, IdGenre = 1 },
            new BookGenre { IdBook = 2, IdGenre = 2 },
            new BookGenre { IdBook = 3, IdGenre = 3 }
        );
            
        modelBuilder.Entity<BookAuthor>().HasData(
            new BookAuthor { IdBook = 1, IdAuthor = 1 },
            new BookAuthor { IdBook = 2, IdAuthor = 2 },
            new BookAuthor { IdBook = 3, IdAuthor = 3 }
        );
        
        modelBuilder.Entity<Book>().HasData(
            new Book { IdBook = 1, Name = "Book One", ReleaseDate = new DateTime(2020, 1, 1), IdPublishingHouse = 1 },
            new Book { IdBook = 2, Name = "Book Two", ReleaseDate = new DateTime(2021, 2, 2), IdPublishingHouse = 2 },
            new Book { IdBook = 3, Name = "Book Three", ReleaseDate = new DateTime(2022, 3, 3), IdPublishingHouse = 3 }
        );
        
        modelBuilder.Entity<Author>().HasData(
            new Author { IdAuthor = 1, FirstName = "John", LastName = "Doe" },
            new Author { IdAuthor = 2, FirstName = "Jane", LastName = "Smith" },
            new Author { IdAuthor = 3, FirstName = "Alice", LastName = "Johnson" }
        );
        
        modelBuilder.Entity<Genre>().HasData(
            new Genre { IdGenre = 1, Name = "Fiction" },
            new Genre { IdGenre = 2, Name = "Non-Fiction" },
            new Genre { IdGenre = 3, Name = "Science Fiction" }
        );
        
        modelBuilder.Entity<PublishingHouse>().HasData(
            new PublishingHouse { IdPublishingHouse = 1, Name = "Publisher One", Country = "USA", City = "New York" },
            new PublishingHouse { IdPublishingHouse = 2, Name = "Publisher Two", Country = "UK", City = "London" },
            new PublishingHouse { IdPublishingHouse = 3, Name = "Publisher Three", Country = "Canada", City = "Toronto" }
        );
    }
}