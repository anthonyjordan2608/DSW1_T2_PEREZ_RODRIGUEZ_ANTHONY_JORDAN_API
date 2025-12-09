using Library.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto?> GetBookByIdAsync(int id);
        Task<BookDto> CreateBookAsync(CreateBookDto createBookDto);
        Task<BookDto?> UpdateBookAsync(int id, CreateBookDto updateBookDto);
        Task<bool> DeleteBookAsync(int id);
    }
}
