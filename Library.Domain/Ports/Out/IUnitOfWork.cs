namespace Library.Domain.Ports.Out
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository BookRepository { get; }
        ILoanRepository LoanRepository { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
