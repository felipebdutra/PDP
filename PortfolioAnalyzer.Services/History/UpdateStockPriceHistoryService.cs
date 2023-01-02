using PortfolioAnalyzer.Core.PortfolioAggregate;

namespace PortfolioAnalyzer.Services.History;

public class UpdateStockPriceHistoryService : IUpdateStockPriceHistoryService
{
    private IPortfolioService _portfolioService { get; set; }
    private IStockPriceHistoryService _stockPriceHistoryService { get; set; }

    public UpdateStockPriceHistoryService(
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


    public async Task<StockPricesHistory> GetInstrumentStoredPricesAsync()
    {
        return await _stockPriceHistoryService.GetLatestHistoricClosingDateAsync();
    }
}
