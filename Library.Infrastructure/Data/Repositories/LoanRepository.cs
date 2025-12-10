using Microsoft.EntityFrameworkCore;
using Library.Domain.Entities;
using Library.Domain.Ports.Out;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories
{
    public class LoanRepository : Repository<Loan>, ILoanRepository
    {
        public LoanRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _context.Loans
                .Include(l => l.Book)  
                .ToListAsync();
        }

        public override async Task<Loan?> GetByIdAsync(int id)
        {
            return await _context.Loans
                .Include(l => l.Book)  
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Loan>> GetActiveLoansAsync()
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Where(l => l.Status == "Active")
                .OrderByDescending(l => l.LoanDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Loan>> GetLoansByBookIdAsync(int bookId)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Where(l => l.BookId == bookId)
                .ToListAsync();
        }

        public async Task<Loan?> GetActiveLoanByBookIdAsync(int bookId)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .FirstOrDefaultAsync(l => l.BookId == bookId && l.Status == "Active");
        }
    }
}