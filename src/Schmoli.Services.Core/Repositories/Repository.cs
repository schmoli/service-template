using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Schmoli.Services.Core.Requests;
using Schmoli.Services.Core.Results;

namespace Schmoli.Services.Core.Repositories
{

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext Context { get; }

        public Repository(DbContext context)
        {
            Context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }

        public Task<PagedResultSet<TEntity>> Search(PagedRequest pagedRequest, Func<IQueryable<TEntity>, IQueryable<TEntity>> query)
        {
            return PagedResultSet<TEntity>.CreateAsync(
                    query(Context.Set<TEntity>().AsQueryable()),
                    pagedRequest.PageNumber,
                    pagedRequest.PageSize,
                    x => x.ToListAsync());
        }

        public Task<PagedResultSet<TEntity>> Search(PagedRequest pagedRequest, Expression<Func<TEntity, bool>> predicate)
        {
            var query = Context.Set<TEntity>().Where(predicate);
            return PagedResultSet<TEntity>.CreateAsync(
                    query,
                    pagedRequest.PageNumber,
                    pagedRequest.PageSize,
                    x => x.ToListAsync());
        }

        public Task<PagedResultSet<TEntity>> GetAllAsync(PagedRequest pagedRequest)
        {
            var query = Context.Set<TEntity>();
            return PagedResultSet<TEntity>.CreateAsync(
                query,
                pagedRequest.PageNumber,
                pagedRequest.PageSize,
                x => x.ToListAsync());
        }

        public ValueTask<TEntity> GetByIdAsync(long id)
        {
            return Context.Set<TEntity>().FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        public Task<bool> Any(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).AnyAsync();
        }
    }
}
