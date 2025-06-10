using APBD_Test2.Contracts.Requests;
using APBD_Test2.Contracts.Responses;
using APBD_Test2.Data;
using APBD_Test2.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_Test2.Services;

public class BookService : IBookService
{
    private readonly DatabaseContext _context;
    
    public BookService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<List<GetListOfAllBooksResponse.BookResponse>> GetListOfAllBooksAsync(string? name, string? author, string? genre)
    {
        var query = _context.Books
            .Include(b => b.BookAuthors)
            .ThenInclude(ba => ba.Author)
            .Include(b => b.BookGenres)
            .AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(b => b.Name == name);
        }
        
        if (!string.IsNullOrEmpty(author))
        {
            query = query.Where(b => b.BookAuthors.Any(ba => ba.Author.FirstName == author));
        }
        
        if (!string.IsNullOrEmpty(genre))
        {
            query = query.Where(b => b.BookGenres.Any(bg => bg.Genre.Name == genre));
        }
        
        return await query.OrderByDescending(b => b.ReleaseDate)
            .Select(b => new GetListOfAllBooksResponse.BookResponse
            {
                IdBook = b.IdBook,
                Name = b.Name,
                ReleaseDate = b.ReleaseDate,
                BookGenres = b.BookGenres.Select(bg => bg.Genre.Name).ToList(),
                BookAuthors = b.BookAuthors.Select(ba => ba.Book.Name).ToList(),
            }).ToListAsync();
    }

    public async Task<CreateBookResponse> CreateBookAsync(Book request)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var book = new Models.Book
            {
                Name = request.Name,
                ReleaseDate = request.ReleaseDate,
                IdPublishingHouse = request.IdPublishingHouse
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            foreach (var authorId in request.BookAuthors)
            {

                if (book != null)
                {
                    throw new Exception($"Book with Id {book.IdBook} exists.");
                }

                var bookAuthor = new Models.BookAuthor
                {
                    IdBook = book.IdBook,
                    IdAuthor = book.BookAuthors.First().IdAuthor,
                };
                _context.BookAuthors.Add(bookAuthor);
            }

            foreach (var genreId in request.BookGenres)
            {
                var bookGenre = new Models.BookGenre
                {
                    IdBook = book.IdBook,
                    IdGenre = book.BookGenres.First().IdGenre,
                };
                _context.BookGenres.Add(bookGenre);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return new CreateBookResponse
            {
                Message = "Book created successfully",
                Book = new CreateBookResponse.BookResponse
                {
                    IdBook = book.IdBook,
                    Name = book.Name,
                    ReleaseDate = book.ReleaseDate,
                    IdPublishingHouse = book.IdPublishingHouse,
                    Authors = book.BookAuthors.Select(ba => new CreateBookResponse.BookResponse
                    {
                        IdAuthor = ba.IdAuthor,
                        FirstName = ba.Author.FirstName,
                        LastName = ba.Author.LastName
                    }).ToList(),
                    Genres = book.BookGenres.Select(bg => new CreateBookResponse.BookResponse
                    {
                        IdGenre = bg.IdGenre,
                        Name = bg.Genre.Name
                    }).ToList()
                }
            };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception("An error occurred while creating the book.", ex);
        }
    }
}