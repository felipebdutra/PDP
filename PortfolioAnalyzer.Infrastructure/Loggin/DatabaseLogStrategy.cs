using MongoDB.Driver;
using System.Text;

namespace PortfolioAnalyzer.Infrastructure.Loggin
{
    public class DatabaseLogStrategy 
    {
        public int MyProperty { get; set; }


        public DatabaseLogStrategy(IMongoDatabase database)
        {

        }

            public void WriteLog(StringBuilder message)
        {
            throw new NotImplementedException();
        }
    }
}
