using System.Collections.Concurrent;

namespace PortfolioAnalyzer.Infrastructure.Integration.Api;

public interface IStockMarketApiClient
{
    int ChunkSize { get; }
    int AuthorizationDelay { get; }
    bool RequiresAutorization { get; }

    Task<decimal> GetClosePriceAsync(string ticker);
}
