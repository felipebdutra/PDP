using MongoDB.Driver;
using PortfolioAnalyzer.Core.BankAggregate;

namespace PortfolioAnalyzer.Repository.Bank
{
    public class BankRepository : RepositoryBase<Core.BankAggregate.Bank>, IBankRepository
    {
        public BankRepository(IMongoDatabase database)
            : base(
                database,
                RepositoryConstants.MongoDb.Database.PortfolioAnalyzer.Collections.Bank
            )
        {
        }

        public async Task<IList<BankAccount>> GetAllAccountsAsync()
        {
            var accounts = new List<BankAccount>();

            var findOptions = new FindOptions<Core.BankAggregate.Bank>()
            {
                Projection = Builders<Core.BankAggregate.Bank>.Projection.Include(s => s.Accounts).Exclude("_id")
            };

            var filter = Builders<Core.BankAggregate.Bank>.Filter.Where(d => d.Accounts.Count > 0);

            using (var cursor = await _collection.FindAsync(filter, findOptions))
            {
                while (await cursor.MoveNextAsync())
                {
                    foreach (Core.BankAggregate.Bank bank in cursor.Current)
                    {
                        accounts.AddRange(bank.Accounts);
                    }
                }
            }

            return accounts;
        }
    }
}
