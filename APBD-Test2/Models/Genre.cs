using System.ComponentModel.DataAnnotations;

namespace APBD_Test2.Models;

public class Genre
{
    [Key]
    public int IdGenre { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    
    public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
}