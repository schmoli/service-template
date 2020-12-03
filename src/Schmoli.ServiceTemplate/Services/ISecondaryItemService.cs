using System.Threading.Tasks;
using Schmoli.Services.Core.Results;
using Schmoli.ServiceTemplate.Models;
using Schmoli.ServiceTemplate.Requests;

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
