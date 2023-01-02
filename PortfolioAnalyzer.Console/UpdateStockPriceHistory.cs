using PortfolioAnalyzer.Core.PortfolioAggregate;

namespace PortfolioAnalyzer.Console;

public class UpdateStockPriceHistory
{
    private IPortfolioService _portfolioService { get; set; }
    private IStockPriceHistoryService _stockPriceHistoryService { get; set; }

    public UpdateStockPriceHistory(
        IPortfolioService portfolioService,
        IStockPriceHistoryService stockPriceHistoryService
    )
    {
        _portfolioService = portfolioService;
        _stockPriceHistoryService = stockPriceHistoryService;
    }

    public async Task<StockPricesHistory> GetInstrumentCurrentPricesAsync()
    {
        var tickers = await _portfolioService.GetTickersAsync();

        return await _stockPriceHistoryService.SyncLatestClosingPricesAsync(tickers);
    }
}
