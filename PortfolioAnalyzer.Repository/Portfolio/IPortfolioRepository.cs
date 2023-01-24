using PortfolioAnalyzer.Infrastructure.Repository;

namespace PortfolioAnalyzer.Repository.Portfolio;
public interface IPortfolioRepository : IRepositoryBase<Core.PortfolioAggregate.Portfolio>
{
    public Task<IEnumerable<string>> GetTickersAsync();
    public Task<List<Core.PortfolioAggregate.Portfolio>> GetAllPortoliosAsync();
}