using Microsoft.EntityFrameworkCore;
using Library.Domain.Entities;
using Library.Domain.Ports.Out;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Book?> GetByISBNAsync(string isbn)
        {
            return await _context.Books
                .FirstOrDefaultAsync(b => b.ISBN == isbn);
        }

        public async Task<bool> IsISBNUniqueAsync(string isbn, int? excludedId = null)
        {
            var query = _context.Books.Where(b => b.ISBN == isbn);

            if (excludedId.HasValue)
            {
                query = query.Where(b => b.Id != excludedId.Value);
            }

            return !await query.AnyAsync();
        }

        public async Task UpdateStockAsync(int bookId, int quantity)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                book.Stock += quantity;
                if (book.Stock < 0) book.Stock = 0;
                _context.Books.Update(book);
            }
        }
    }
}
