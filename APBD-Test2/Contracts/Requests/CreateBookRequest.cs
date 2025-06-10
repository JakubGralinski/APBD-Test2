using System.ComponentModel.DataAnnotations;
using APBD_Test2.Models;

namespace APBD_Test2.Contracts.Requests;

public class CreateBookRequest
{
    [Key]
    public int IdBook { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    
    [Required]
    public DateTime ReleaseDate { get; set; }
    
    [Required]
    public int IdPublishingHouse { get; set; }
    
    public ICollection<BookAuthor> Authors { get; set; } = new List<BookAuthor>();
    public ICollection<BookGenre> Genres { get; set; } = new List<BookGenre>();
    
    //[Required] public List<BookListRequest> BookListRequests { get; set; } = new List<BookListRequest>();
}

public class BookListRequest
{
    [Required]
    public int IdBook { get; set; }
    
    [Required]
    public string AuthorFirstName { get; set; }
    
    [Required]
    public string AuthorLastName { get; set; }
    
    [Required]
    public string GenreName { get; set; }
}