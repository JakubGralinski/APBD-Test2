using System.ComponentModel.DataAnnotations;
using APBD_Test2.Models;

namespace APBD_Test2.Contracts.Responses;

public class CreateBookResponse
{
    [Required]
    public string Message { get; set; }
    
    [Required]
    public BookResponse Book { get; set; }

    //public int IdPublishingHouse { get; set; }
    //public string Name { get; set; }
    //public DateTime ReleaseDate { get; set; }
    //public List<BookResponse> Authors { get; set; }
    //public List<BookResponse> Genres { get; set; }

    public class BookResponse
    {
        [Key]
        public int IdBook { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        
        [Required]
        public DateTime ReleaseDate { get; set; }
        
        [Required]
        public int IdPublishingHouse { get; set; }
        
        //public List<BookResponse> Authors { get; set; } = new List<BookAuthor>();
        //public List<BookResponse> Genres { get; set; } = new List<BookResponse>();
        public int IdAuthor { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IdGenre { get; set; }
        public List<BookResponse> Authors { get; set; }
        public List<BookResponse> Genres { get; set; }
    }
}