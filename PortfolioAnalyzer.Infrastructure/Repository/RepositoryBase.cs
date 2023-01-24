using MongoDB.Driver;

namespace PortfolioAnalyzer.Infrastructure.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly IMongoCollection<T> _collection;

        protected RepositoryBase(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<IEnumerable<T>> FindAsync(FilterDefinition<T> filter = null, FindOptions<T> options = null)
        {
            filter ??= Builders<T>.Filter.Empty;

            var result = new List<T>();

            using (var cursor = await _collection.FindAsync<T>(filter, options))
            {
                while (await cursor.MoveNextAsync())
                {
                    result.AddRange(cursor.Current);
                }
            }

            return result;
        }

        public List<T> Find(FilterDefinition<T> filter = null, FindOptions options = null)
        {
            filter ??= Builders<T>.Filter.Empty;

            return _collection.Find(filter, options).ToList();
        }

        public async Task InsertAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public void Insert(T entity)
        {
            _collection.InsertOne(entity);
        }

        public UpdateResult Update(
            FilterDefinition<T> filter,
            UpdateDefinition<T> update,
            UpdateOptions options = null
        )
        {
            return _collection.UpdateOne(filter, update);
        }

        public async Task<UpdateResult> UpdateAsync(
          FilterDefinition<T> filter,
          UpdateDefinition<T> update,
          UpdateOptions options = null
      )
        {
            return await _collection.UpdateOneAsync(filter, update, options);
        }

        public DeleteResult Delete(FilterDefinition<T> filter)
        {
            return _collection.DeleteOne(filter);
        }

        public async Task<DeleteResult>DeleteAsync(FilterDefinition<T> filter)
        {
            return await _collection.DeleteOneAsync(filter);
        }
    }
}
