namespace QuoteCalculator.Domain
{
    public interface IUnitOfWork
    {
        IRepository<Loan> LoanRepository { get; }
        void SaveChanges();
    }
}
