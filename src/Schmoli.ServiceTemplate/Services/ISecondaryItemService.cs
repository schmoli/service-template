using System.Threading.Tasks;
using Schmoli.ServiceTemplate.Models;
using Schmoli.ServiceTemplate.Requests;
using Schmoli.Services.Core.Results;

namespace Schmoli.ServiceTemplate.Services
{
    public interface ISecondaryItemService
    {
        Task<PagedResultSet<SecondaryItem>> Search(SecondaryItemPagedRequest pagedRequest);
        Task<SecondaryItem> GetById(long id);
        Task<SecondaryItem> Create(SecondaryItem newItem);
        Task Update(SecondaryItem itemToUpdate, SecondaryItem item);
        Task Delete(SecondaryItem item);
    }
}
