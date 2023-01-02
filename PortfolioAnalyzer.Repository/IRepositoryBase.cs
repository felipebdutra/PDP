using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace PortfolioAnalyzer.Repository
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IEnumerable<T>> FindAsync(FilterDefinition<T> filter = null);
        List<T> Find(FilterDefinition<T> filter = null, FindOptions options = null);

        void Insert(T entity);

        UpdateResult Update(FilterDefinition<T> filter,UpdateDefinition<T> update,UpdateOptions options);

        DeleteResult Delete(FilterDefinition<T> filter);
    }
}