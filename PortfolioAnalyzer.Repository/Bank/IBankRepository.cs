using PortfolioAnalyzer.Core.BankAggregate;
using PortfolioAnalyzer.Infrastructure.Repository;

namespace PortfolioAnalyzer.Repository.Bank
{
    public interface IBankRepository : IRepositoryBase<Core.BankAggregate.Bank>
    {
        Task<IList<BankAccount>> GetAllAccountsAsync();
        Task AddBankAccount(BankAccount account, string bank);
        Task UpdateBankAccountAmount(string bankName, string currency, decimal amount);
    }
}
