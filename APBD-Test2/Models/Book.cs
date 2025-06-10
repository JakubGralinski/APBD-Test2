using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_Test2.Models;

public class Book
{
    [Key]
    public int IdBook { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    
    [Required]
    public DateTime ReleaseDate { get; set; }
    
    [ForeignKey("IdPublishingHouse")] public PublishingHouse PublishingHouse { get; set; } = null!;
    
    [Required]
    public int IdPublishingHouse { get; set; }
    
    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
}