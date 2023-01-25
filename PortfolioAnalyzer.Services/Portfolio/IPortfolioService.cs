using MongoDB.Driver;
using PortfolioAnalyzer.Core.PortfolioAggregate;
using PortfolioAnalyzer.Repository.Portfolio;

public interface IPortfolioService
{
    Task<IEnumerable<string>> GetTickersAsync();
    Task<List<PortfolioDto>> GetAllPortfoliosAsync();
    Task<List<Portfolio>> GetPortfoliosAsync();
}