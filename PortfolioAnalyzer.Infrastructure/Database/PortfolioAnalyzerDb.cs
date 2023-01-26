using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace PortfolioAnalyzer.Infrastructure.Database
{
    public class PortfolioAnalyzerDb
    {
        public IMongoDatabase GetDatabase(IConfiguration configuration, string name)
        {
            return new MongoClient(configuration["MongoDb_ConnectionString"]).GetDatabase(name);                
        }
    }
}
