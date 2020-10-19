using System.Threading.Tasks;
using Schmoli.ServiceTemplate.Models;
using Schmoli.ServiceTemplate.Requests;
using Schmoli.Services.Core.Requests;
using Schmoli.Services.Core.Results;

namespace Schmoli.ServiceTemplate.Services
{

    public interface IPrimaryItemService
    {
        Task<PagedResultSet<PrimaryItem>> GetPagedResultsWithSecondaryItem(PrimaryItemPagedRequest pagedRequest);
        Task<PrimaryItem> GetById(long id);
        Task<PrimaryItem> GetByName(string name);
        Task<PagedResultSet<PrimaryItem>> GetBySecondaryItemId(long secondaryItemId, PagedRequest pagedRequest);
        Task<PrimaryItem> Create(PrimaryItem newItem);
        Task Update(PrimaryItem originalItem, PrimaryItem updatedItem);
        Task Delete(PrimaryItem item);
    }
}
