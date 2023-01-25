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

    public async Task<IEnumerable<string>> GetTickersAsync()
    {
        return await _repository.GetTickersAsync();
    }

    public async Task<List<PortfolioDto>> GetAllPortfoliosAsync()
    {
        return await _repository.GetAllPortoliosAsync();
    }

    public async Task<List<Core.PortfolioAggregate.Portfolio>> GetPortfoliosAsync()
    {
        return await _repository.GetPortoliosAsync();
    }
}
