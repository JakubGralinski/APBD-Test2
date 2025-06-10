using System.ComponentModel.DataAnnotations;

namespace APBD_Test2.Models;

public class Author
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