using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Schmoli.Services.Core.Repositories;
using Schmoli.Services.Core.Requests;
using Schmoli.Services.Core.Results;
using Schmoli.ServiceTemplate.Data;
using Schmoli.ServiceTemplate.Models;
using Schmoli.ServiceTemplate.Requests;

namespace Schmoli.ServiceTemplate.Repositories
{
    public class PrimaryItemRepository : Repository<PrimaryItem>, IPrimaryItemRepository
    {
        public PrimaryItemRepository(ServiceDbContext context)
            : base(context) { }

        private ServiceDbContext ServiceContext => Context as ServiceDbContext;

        public Task<PagedResultSet<PrimaryItem>> GetAllWithSecondaryItemPagedAsync(PrimaryItemPagedRequest pagedRequest)
        {
            var query = ServiceContext.PrimaryItems
                .Include(x => x.SecondaryItem)
                .Where(x => string.IsNullOrWhiteSpace(pagedRequest.Query) || x.Name.Contains(pagedRequest.Query))
                .OrderBy(pagedRequest.OrderBy + $" {pagedRequest.SortOrder}");
            return PagedResultSet<PrimaryItem>.CreateAsync(query, pagedRequest.PageNumber, pagedRequest.PageSize, x => x.ToListAsync());
        }

        public Task<PagedResultSet<PrimaryItem>> GetAllWithSecondaryItemBySecondaryItemIdAsync(long secondaryItemId, PagedRequest pagedRequest)
        {
            var query = ServiceContext.PrimaryItems
                .Include(e => e.SecondaryItem)
                .Where(e => e.SecondaryItemId == secondaryItemId);

            return PagedResultSet<PrimaryItem>.CreateAsync(query, pagedRequest.PageNumber, pagedRequest.PageSize, x => x.ToListAsync());
        }

        public async Task<PrimaryItem> GetWithSecondaryItemByIdAsync(long id)
        {
            return await ServiceContext.PrimaryItems
                .Include(e => e.SecondaryItem)
                .SingleOrDefaultAsync(e => e.Id == id);
        }
    }
}
