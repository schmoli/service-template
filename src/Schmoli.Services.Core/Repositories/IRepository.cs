using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Schmoli.Services.Core.Requests;
using Schmoli.Services.Core.Results;

namespace Schmoli.Services.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get TEntity by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ValueTask<TEntity> GetByIdAsync(long id);

        /// <summary>
        /// Return all TEntity, using paging
        /// </summary>
        /// <param name="pagedRequest"></param>
        /// <returns></returns>
        Task<PagedResultSet<TEntity>> GetAllAsync(PagedRequest pagedRequest);

        /// <summary>
        /// Search for TEntity[] using a simple predicate expression
        /// </summary>
        /// <param name="pagedRequest"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<PagedResultSet<TEntity>> Search(PagedRequest pagedRequest,
                                           Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Search for TEntity[] using a complex query
        /// </summary>
        /// <param name="pagedRequest"></param>
        /// <param name="query">query to execute against TEntity</param>
        /// <returns></returns>
        Task<PagedResultSet<TEntity>> Search(PagedRequest pagedRequest,
                                             Func<IQueryable<TEntity>, IQueryable<TEntity>> query);

        /// <summary>
        /// Return a single TEntity based on the predicate passed in.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Return true if there exists any TEntity based on the predicate pased in.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<bool> Any(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Add TEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Add collection of TEntity
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Remove TEntity
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TEntity entity);

        /// <summary>
        /// Remove a collection of TEntity
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
