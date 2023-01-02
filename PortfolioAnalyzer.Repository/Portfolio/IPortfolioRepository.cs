namespace PortfolioAnalyzer.Repository.Portfolio;
public interface IPortfolioRepository
{
    public Task<IEnumerable<string>> GetTickersAsync();
    public Task<List<Core.PortfolioAggregate.Portfolio>> GetAllPortoliosAsync();
}