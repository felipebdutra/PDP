using MongoDB.Driver;
using PortfolioAnalyzer.Core.PortfolioAggregate;
using PortfolioAnalyzer.Infrastructure.Repository;

namespace PortfolioAnalyzer.Repository.Portfolio;

public class PortfolioRepository : RepositoryBase<Core.PortfolioAggregate.Portfolio>, IPortfolioRepository
{
    private readonly IMongoCollection<Core.PortfolioAggregate.Portfolio> _portfolio;

    public PortfolioRepository(IMongoDatabase database) : base(database, RepositoryConstants.MongoDb.Database.PortfolioAnalyzer.Collections.Portfolio)
    {
        _portfolio = database.GetCollection<Core.PortfolioAggregate.Portfolio>(
            RepositoryConstants.MongoDb.Database.PortfolioAnalyzer.Collections.Portfolio
        );
    }

    public async Task<IEnumerable<string>> GetTickersAsync()
    {

        var distinct = await _portfolio.DistinctAsync(new StringFieldDefinition<Core.PortfolioAggregate.Portfolio, string>("Positions.Ticker"),
          FilterDefinition<Core.PortfolioAggregate.Portfolio>.Empty);

        var result = new List<string>();
        while (await distinct.MoveNextAsync())
        {
            foreach (var tickers in distinct.Current)
            {
                result.Add(tickers);
            }
        }

        return result;
    }

    public async Task<List<PortfolioDto>> GetAllPortoliosAsync()
    {
        var portfolios = await _collection.FindAsync(Builders<Core.PortfolioAggregate.Portfolio>.Filter.Empty);

        var result = new List<PortfolioDto>();
        while (await portfolios.MoveNextAsync())
        {
            foreach (var portfolio in portfolios.Current)
            {
                result.Add(new PortfolioDto()
                {
                    Broker = portfolio.Broker,
                    Positions = portfolio.Positions != null ? portfolio.Positions.ToList() : null,
                });
            }
        }

        return result;
    }

    public async Task<List<Core.PortfolioAggregate.Portfolio>> GetPortoliosAsync()
    {
        var portfolios = await _collection.FindAsync(Builders<Core.PortfolioAggregate.Portfolio>.Filter.Empty);

        var result = new List<Core.PortfolioAggregate.Portfolio>();
        while (await portfolios.MoveNextAsync())
        {
            result.AddRange(portfolios.Current);
        }

        return result;
    }
}
