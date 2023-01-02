using PortfolioAnalyzer.Core.PortfolioAggregate;

public interface IStockPriceHistoryService
{
    Task<StockPricesHistory> SyncLatestClosingPricesAsync(IEnumerable<string> tickers);
    Task<StockPricesHistory> GetLatestHistoricClosingDateAsync();
}