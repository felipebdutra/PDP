using PortfolioAnalyzer.Core.BankAggregate;
using PortfolioAnalyzer.Infrastructure.Repository;

namespace PortfolioAnalyzer.Repository.Bank
{
    public interface IBankRepository : IRepositoryBase<Core.BankAggregate.Bank>
    {
        Task<IList<BankAccount>> GetAllAccountsAsync();
    }
}
