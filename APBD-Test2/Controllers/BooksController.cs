using APBD_Test2.Contracts.Requests;
using APBD_Test2.Models;
using APBD_Test2.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Test2.Controllers;

public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    
    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }
    
    [HttpGet("books")]
    public async Task<IActionResult> GetListOfBooks([FromQuery] string? title, [FromQuery] string? author, [FromQuery] string? genre)
    {
        var books = await _bookService.GetListOfAllBooksAsync(title, author, genre);
        return Ok(books);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] Book book)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var response = await _bookService.CreateBookAsync(book);
            return CreatedAtAction("GetListOfBooks", "Books", new { }, response);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}