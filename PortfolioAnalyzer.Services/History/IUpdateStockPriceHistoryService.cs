using PortfolioAnalyzer.Core.PortfolioAggregate;

namespace PortfolioAnalyzer.Services.History
{
    public interface IUpdateStockPriceHistoryService
    {
        Task<StockPricesHistory> GetInstrumentCurrentPricesAsync();
        Task<StockPricesHistory> GetInstrumentStoredPricesAsync();
    }
}
