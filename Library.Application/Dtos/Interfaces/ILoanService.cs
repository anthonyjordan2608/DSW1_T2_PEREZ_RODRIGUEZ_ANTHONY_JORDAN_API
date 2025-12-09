using Library.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface ILoanService
    {
        Task<IEnumerable<LoanDto>> GetAllLoansAsync();
        Task<IEnumerable<LoanDto>> GetActiveLoansAsync();
        Task<LoanDto?> GetLoanByIdAsync(int id);
        Task<LoanDto> CreateLoanAsync(CreateLoanDto createLoanDto);
        Task<LoanDto?> ReturnLoanAsync(int loanId);
    }
}