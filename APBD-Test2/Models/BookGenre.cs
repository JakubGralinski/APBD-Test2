using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_Test2.Models;

public class BookGenre
{
    [Key]
    public int IdGenre { get; set; }
    
    [Key]
    public int IdBook { get; set; }
    
    [ForeignKey("IdGenre")] public Genre Genre { get; set; } = null!;
    [ForeignKey("IdBook")] public Book Book { get; set; } = null!;
}