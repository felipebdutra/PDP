using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
