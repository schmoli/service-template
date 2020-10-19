using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schmoli.Services.Core.Results
{
    public class PagedResultSet<T> : PagedResultSetBase where T : class
    {
        public IEnumerable<T> Items { get; set; }

        private PagedResultSet()
        {

        }

        public PagedResultSet(IEnumerable<T> pageItems, int totalItemCount, int pageNumber, int pageSize)
        {
            Items = pageItems.ToList();
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalItemCount = totalItemCount;
            TotalPageCount = (int)Math.Ceiling(TotalItemCount / (double)PageSize);
        }

        public static async Task<PagedResultSet<T>> CreateAsync(IQueryable<T> query, int pageNumber, int pageSize, Func<IQueryable<T>, Task<List<T>>> func)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber));
            }
            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            }

            int totalItemCount = query.Count();

            IQueryable<T> queryable = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            List<T> items = await func.Invoke(queryable);

            return new PagedResultSet<T>(items, totalItemCount, pageNumber, pageSize);
        }
    }
}
