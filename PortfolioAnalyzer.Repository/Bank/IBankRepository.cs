using PortfolioAnalyzer.Core.BankAggregate;

namespace PortfolioAnalyzer.Repository.Bank
{
    public interface IBankRepository : IRepositoryBase<Core.BankAggregate.Bank>
    {
        Task<IList<BankAccount>> GetAllAccountsAsync();
    }
}
