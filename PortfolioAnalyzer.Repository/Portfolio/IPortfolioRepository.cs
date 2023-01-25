using PortfolioAnalyzer.Infrastructure.Repository;

namespace PortfolioAnalyzer.Repository.Portfolio;
public interface IPortfolioRepository : IRepositoryBase<Core.PortfolioAggregate.Portfolio>
{
    Task<IEnumerable<string>> GetTickersAsync();
    Task<List<PortfolioDto>> GetAllPortoliosAsync();
    Task<List<Core.PortfolioAggregate.Portfolio>> GetPortoliosAsync();
}