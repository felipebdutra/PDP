using MongoDB.Driver;
using PortfolioAnalyzer.Repository.Portfolio;

namespace PortfolioAnalyzer.Services.Portfolio;

public class PortfolioService : IPortfolioService
{
    public IPortfolioRepository _repository { get; set; }

    public PortfolioService(IPortfolioRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<string>> GetTickersAsync()
    {
        return _repository.GetTickersAsync();
    }

    public Task<List<PortfolioDto>> GetAllPortfoliosAsync()
    {
        return _repository.GetAllPortoliosAsync();
    }

    public Task<List<Core.PortfolioAggregate.Portfolio>> GetPortfoliosAsync()
    {
        return _repository.GetPortoliosAsync();
    }
}
