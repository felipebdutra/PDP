using PortfolioAnalyzer.Core.PortfolioAggregate;
using PortfolioAnalyzer.Infrastructure.Integration.Api;
using PortfolioAnalyzer.Infrastructure.Logging;
using PortfolioAnalyzer.Repository.Portfolio;

public class StockPriceHistoryService : IStockPriceHistoryService
{
    private readonly IStockPricesHistoryRepository _stockPricesHistoryRepository;
    private readonly IStockMarketApiClient _stockMarketApiClient;
    private readonly LogBuilder _logBuilder;

    public StockPriceHistoryService(
        IStockPricesHistoryRepository stockPricesHistoryRepository,
        IStockMarketApiClient stockMarketApiClient,
        LogBuilder logBuilder
    )
    {
        _stockPricesHistoryRepository = stockPricesHistoryRepository;
        _stockMarketApiClient = stockMarketApiClient;
        _logBuilder = logBuilder;
    }

    public Task<StockPricesHistory> GetLatestHistoricClosingDateAsync()
    {
        return _stockPricesHistoryRepository.GetLatestHistoricClosingDateAsync();
    }

    public async Task<StockPricesHistory> SyncLatestClosingPricesAsync(IEnumerable<string> tickers)
    {
        var latestPrices = await GetMarketClosePricesAsync(tickers);

        return await _stockPricesHistoryRepository.InsertAsync(latestPrices);
    }

    private async Task<List<FinancialInstrument>> GetMarketClosePricesAsync(
        IEnumerable<string> tickers
    )
    {
        var prices = new List<FinancialInstrument>();
        int totalReturned = 0;
        int totalSearches = tickers.Count();
        var chunk = tickers.Chunk<string>(_stockMarketApiClient.ChunkSize);

        _logBuilder.WriteLog(new ConsoleLogStrategy(), "Loading stock prices, please wait...");

        foreach (var tickerChunkOf5 in chunk)
        {
            if (_stockMarketApiClient.RequiresAuthorization && prices.Any())
                await WaitForAuthorization();

            await Parallel.ForEachAsync(
                tickerChunkOf5,
                async (ticker, cancellation) =>
                {
                    prices.Add(
                        new()
                        {
                            Ticker = ticker,
                            Total = await _stockMarketApiClient.GetClosePriceAsync(ticker)
                        }
                    );
                }
            );

            totalReturned += _stockMarketApiClient.ChunkSize;

            string logMessage = (totalReturned < totalSearches) ? $"{(totalReturned * 100) / totalSearches}% loaded " : "100% loaded";

            _logBuilder.WriteLog(logMessage);

        }
        return prices;
    }

    private async Task WaitForAuthorization()
    { //Delay due to free request limit
        await Task.Delay(_stockMarketApiClient.AuthorizationDelay);
    }
}
