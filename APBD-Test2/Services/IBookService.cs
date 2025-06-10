using APBD_Test2.Contracts.Requests;
using APBD_Test2.Contracts.Responses;
using APBD_Test2.Models;

namespace APBD_Test2.Services;

public interface IBookService
{
    Task<List<GetListOfAllBooksResponse.BookResponse>> GetListOfAllBooksAsync(string? name, string? author, string? genre);
    Task<CreateBookResponse> CreateBookAsync(Book request);
}