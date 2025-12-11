using Library.Domain.Entities;

namespace Library.Domain.Ports.Out
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<Book?> GetByISBNAsync(string isbn);
        Task<bool> IsISBNUniqueAsync(string isbn, int? excludedId = null);
        Task UpdateStockAsync(int bookId, int quantity);
    }
}