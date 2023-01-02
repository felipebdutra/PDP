using MongoDB.Driver;
using PortfolioAnalyzer.Core.PortfolioAggregate;

namespace PortfolioAnalyzer.Repository.Portfolio;

public class DatabaseLOgRepository : RepositoryBase<Core.PortfolioAggregate.Portfolio>, IPortfolioRepository
{
    private readonly IMongoCollection<Position> _positions;

    public DatabaseLOgRepository(IMongoDatabase database) : base(database, RepositoryConstants.MongoDb.Database.PortfolioAnalyzer.Collections.Portfolio)
    {
        _positions = database.GetCollection<Position>(
            RepositoryConstants.MongoDb.Database.PortfolioAnalyzer.Collections.Position
        );
    }

    public async Task<IEnumerable<string>> GetTickersAsync()
    {
        var tickersList = new List<string>();

        var tickers = await _positions.DistinctAsync(
            d => d.Ticker,
            Builders<Position>.Filter.Empty
        );

        while (await tickers.MoveNextAsync())
        {
            tickersList.AddRange(tickers.Current);
        }

        return tickersList;
    }

    public async Task<List<Core.PortfolioAggregate.Portfolio>> GetAllPortoliosAsync()
    {
        return await _collection
            .Aggregate()
            .Lookup<Core.PortfolioAggregate.Portfolio, Position, Core.PortfolioAggregate.Portfolio>(
                _positions,
                d => d.Positions,
                d => d.Id,
                f => f.Positions
            )
            .ToListAsync();
    }
}
