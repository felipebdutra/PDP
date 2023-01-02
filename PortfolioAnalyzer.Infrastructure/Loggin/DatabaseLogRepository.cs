using MongoDB.Driver;
using PortfolioAnalyzer.Infrastructure.Repository;

namespace PortfolioAnalyzer.Infrastructure.Loggin;

public class DatabaseLogRepository : RepositoryBase<DatabaseLog>
{
    private readonly IMongoCollection<DatabaseLog> _logs;

    public DatabaseLogRepository(IMongoDatabase database) : base(database, RepositoryConstants.MongoDb.Database.PortfolioAnalyzer.Collections.ExecutionLog)
    {
        _logs = database.GetCollection<DatabaseLog>(
            RepositoryConstants.MongoDb.Database.PortfolioAnalyzer.Collections.ExecutionLog
        );
    }
}
