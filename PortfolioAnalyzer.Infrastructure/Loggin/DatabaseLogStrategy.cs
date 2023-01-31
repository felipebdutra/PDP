using MongoDB.Driver;
using PortfolioAnalyzer.Infrastructure.Repository;
using System.Text;

namespace PortfolioAnalyzer.Infrastructure.Logging
{
    public class DatabaseLogStrategy : RepositoryBase<DatabaseLog> , ILogStrategy
    {
        public DatabaseLogStrategy(IMongoDatabase database) : 
            base(database, RepositoryConstants.MongoDb.Database.PortfolioAnalyzer.Collections.ExecutionLog)
        {           

        }

        public void WriteLog(StringBuilder message)
        {
            var log = new DatabaseLog()
            {
                Date = DateTime.Now,
                Message = message.ToString()
            };

            Insert(log);
        }
    }
}
