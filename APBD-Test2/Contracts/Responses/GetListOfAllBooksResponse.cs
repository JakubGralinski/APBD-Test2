using System.ComponentModel.DataAnnotations;
using APBD_Test2.Models;

namespace APBD_Test2.Contracts.Responses;

public class GetListOfAllBooksResponse
{
    public class BookResponse
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
        
        //public List<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public List<string> BookGenres { get; set; } = new List<string>();
        public List<string> BookAuthors { get; set; }
    }
    
    public class AuthorResponse
    {
        [Key]
        public int IdAuthor { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        
        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
    
    public class GenreResponse
    {
        [Key]
        public int IdGenre { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}