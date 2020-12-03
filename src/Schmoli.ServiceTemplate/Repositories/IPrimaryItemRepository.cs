using System.Threading.Tasks;
using Schmoli.Services.Core.Repositories;
using Schmoli.Services.Core.Requests;
using Schmoli.Services.Core.Results;
using Schmoli.ServiceTemplate.Models;
using Schmoli.ServiceTemplate.Requests;

namespace Schmoli.ServiceTemplate.Repositories
{
    public interface IPrimaryItemRepository : IRepository<PrimaryItem>
    {
        Task<PagedResultSet<PrimaryItem>> GetAllWithSecondaryItemPagedAsync(PrimaryItemPagedRequest pagedRequest);
        Task<PrimaryItem> GetWithSecondaryItemByIdAsync(long id);
        Task<PagedResultSet<PrimaryItem>> GetAllWithSecondaryItemBySecondaryItemIdAsync(long secondaryItemId, PagedRequest pagedRequest);
    }
}
