using MongoDB.Driver;
using PortfolioAnalyzer.Core.PortfolioAggregate;

public interface IPortfolioService
{
    Task<IEnumerable<string>> GetTickersAsync();
    Task<List<Portfolio>> GetAllPortfoliosAsync();
}