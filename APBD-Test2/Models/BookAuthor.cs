using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_Test2.Models;

public class BookAuthor
{
    [Key]
    public int IdBook { get; set; }
    
    [Key]
    public int IdAuthor { get; set; }
    
    [ForeignKey("IdBook")] public Book Book { get; set; } = null!;
    [ForeignKey("IdAuthor")] public Author Author { get; set; } = null!;
}