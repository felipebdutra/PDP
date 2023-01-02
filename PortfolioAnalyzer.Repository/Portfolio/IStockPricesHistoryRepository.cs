using PortfolioAnalyzer.Core.PortfolioAggregate;

namespace PortfolioAnalyzer.Repository.Portfolio;

public interface IStockPricesHistoryRepository
{
    Task<StockPricesHistory> InsertAsync(List<FinancialInstrument> instruments);
    Task<StockPricesHistory> GetLatestHistoricClosingDateAsync();
}
