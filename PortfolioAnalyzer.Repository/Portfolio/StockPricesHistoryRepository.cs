using System.Drawing;
using System.Collections.Immutable;
using MongoDB.Driver;
using PortfolioAnalyzer.Core.PortfolioAggregate;
using PortfolioAnalyzer.Infrastructure.Repository;

namespace PortfolioAnalyzer.Repository.Portfolio;

public class StockPricesHistoryRepository : IStockPricesHistoryRepository
{
    private IMongoDatabase _database;
    private IMongoCollection<StockPricesHistory> _pricesHistory;

    public StockPricesHistoryRepository(IMongoDatabase database)
    {
        _database = database;
        _pricesHistory = _database.GetCollection<StockPricesHistory>(
            RepositoryConstants.MongoDb.Database.PortfolioAnalyzer.Collections.StockPricesHistory
        );
    }

    public async Task<StockPricesHistory> InsertAsync(List<FinancialInstrument> instruments)
    {
        var document = new StockPricesHistory() { Date = DateTime.Now, Instruments = instruments };

        await _pricesHistory.InsertOneAsync(document);

        return document;
    }

    public async Task<StockPricesHistory> GetLatestHistoricClosingDateAsync()
    {
        return await _pricesHistory
            .Find(Builders<StockPricesHistory>.Filter.Empty)
            .SortByDescending(s => s.Date)
            .FirstOrDefaultAsync();
    }
}
