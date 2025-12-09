using AutoMapper;
using Library.Application.Dtos;
using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Ports.Out;

namespace Library.Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LoanService(
            ILoanRepository loanRepository,
            IBookRepository bookRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<LoanDto>> GetAllLoansAsync()
        {
            var loans = await _loanRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LoanDto>>(loans);
        }

        public async Task<IEnumerable<LoanDto>> GetActiveLoansAsync()
        {
            var loans = await _loanRepository.GetActiveLoansAsync();
            return _mapper.Map<IEnumerable<LoanDto>>(loans);
        }

        public async Task<LoanDto?> GetLoanByIdAsync(int id)
        {
            var loan = await _loanRepository.GetByIdAsync(id);
            return loan == null ? null : _mapper.Map<LoanDto>(loan);
        }

        public async Task<LoanDto> CreateLoanAsync(CreateLoanDto createLoanDto)
        {
            // Validar que el libro exista
            var book = await _bookRepository.GetByIdAsync(createLoanDto.BookId);
            if (book == null)
                throw new KeyNotFoundException($"Libro con ID {createLoanDto.BookId} no encontrado.");

            // Validar stock disponible
            if (book.Stock <= 0)
                throw new InvalidOperationException($"El libro '{book.Title}' no tiene stock disponible.");

            // Crear préstamo
            var loan = _mapper.Map<Loan>(createLoanDto);
            loan.Status = "Active";
            loan.LoanDate = DateTime.UtcNow;

            var createdLoan = await _loanRepository.AddAsync(loan);

            // Disminuir stock del libro
            await _bookRepository.UpdateStockAsync(book.Id, -1);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<LoanDto>(createdLoan);
        }

        public async Task<LoanDto?> ReturnLoanAsync(int loanId)
        {
            var loan = await _loanRepository.GetByIdAsync(loanId);
            if (loan == null || loan.Status == "Returned")
                return null;

            // Marcar como devuelto
            loan.Status = "Returned";
            loan.ReturnDate = DateTime.UtcNow;

            await _loanRepository.UpdateAsync(loan);

            // Aumentar stock del libro
            await _bookRepository.UpdateStockAsync(loan.BookId, 1);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<LoanDto>(loan);
        }
    }
}