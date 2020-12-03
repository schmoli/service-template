using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Schmoli.Services.Core.Exceptions;
using Schmoli.Services.Core.Results;
using Schmoli.ServiceTemplate.Models;
using Schmoli.ServiceTemplate.Requests;

namespace Schmoli.ServiceTemplate.Services
{
    public class SecondaryItemService : ISecondaryItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SecondaryItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SecondaryItem> Create(SecondaryItem newItem)
        {
            // Example of validating: Name is unique, we could catch exception too
            if (await _unitOfWork.SecondaryItems.SingleOrDefaultAsync(x => x.Name.Equals(newItem.Name)) != null)
            {
                throw new ArgumentNotUniqueException(nameof(SecondaryItem.Name));
            }

            newItem.LastUpdated = DateTime.UtcNow;
            await _unitOfWork.SecondaryItems.AddAsync(newItem);
            await _unitOfWork.CommitAsync();

            return newItem;
        }

        public async Task Delete(SecondaryItem item)
        {
            // Example of validating: cascade deletes turned off, no deleting item that is in use by PrimaryEntity
            if (await _unitOfWork.PrimaryItems.Any(x => x.SecondaryItemId == item.Id))
            {
                throw new ArgumentInUseException(nameof(SecondaryItem.Id));
            }
            _unitOfWork.SecondaryItems.Remove(item);
            await _unitOfWork.CommitAsync();
        }

        public async Task<PagedResultSet<SecondaryItem>> Search(SecondaryItemPagedRequest pagedRequest)
        {
            // Build a query specific to our entity and *pagedRequest, then pass query and base request to
            // the core repository implementation to do the work
            IQueryable<SecondaryItem> query(IQueryable<SecondaryItem> query) =>
            query
                .Where(x => string.IsNullOrWhiteSpace(pagedRequest.Query) || x.Name.Contains(pagedRequest.Query))
                .OrderBy($"{pagedRequest.OrderBy} {pagedRequest.SortOrder}");

            return await _unitOfWork.SecondaryItems.Search(pagedRequest, query);
        }

        public async Task<SecondaryItem> GetById(long id)
        {
            return await _unitOfWork.SecondaryItems.GetByIdAsync(id);
        }

        public async Task Update(SecondaryItem itemToUpdate, SecondaryItem item)
        {
            // Example of validating: Name is unique, we could catch exception too
            if (await _unitOfWork.SecondaryItems.SingleOrDefaultAsync(x => x.Name.Equals(item.Name) && x.Id != itemToUpdate.Id) != null)
            {
                throw new ArgumentNotUniqueException(nameof(SecondaryItem.Name));
            }
            itemToUpdate.Name = item.Name;
            itemToUpdate.LastUpdated = DateTime.UtcNow;
            await _unitOfWork.CommitAsync();

        }
    }
}
