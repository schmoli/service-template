using System;
using System.Threading.Tasks;
using Schmoli.Services.Core.Exceptions;
using Schmoli.Services.Core.Requests;
using Schmoli.Services.Core.Results;
using Schmoli.ServiceTemplate.Models;
using Schmoli.ServiceTemplate.Requests;

namespace Schmoli.ServiceTemplate.Services
{
    public class PrimaryItemService : IPrimaryItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PrimaryItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResultSet<PrimaryItem>> GetPagedResultsWithSecondaryItem(PrimaryItemPagedRequest pagedRequest)
        {
            return await _unitOfWork.PrimaryItems.GetAllWithSecondaryItemPagedAsync(pagedRequest);
        }

        public async Task<PrimaryItem> Create(PrimaryItem newItem)
        {
            // Example of validating: Name is unique, we could catch exception too
            if (await _unitOfWork.PrimaryItems.SingleOrDefaultAsync(x => x.Name.Equals(newItem.Name)) != null)
            {
                throw new ArgumentNotUniqueException(nameof(PrimaryItem.Name));
            }

            // Example of valdiating foreign key constraint
            if (await _unitOfWork.SecondaryItems.GetByIdAsync(newItem.SecondaryItemId) == null)
            {
                throw new ArgumentNotFoundException(nameof(PrimaryItem.SecondaryItemId));
            }

            await _unitOfWork.PrimaryItems.AddAsync(newItem);
            await _unitOfWork.CommitAsync();
            return await GetById(newItem.Id);

        }

        public async Task Delete(PrimaryItem entity)
        {
            _unitOfWork.PrimaryItems.Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<PagedResultSet<PrimaryItem>> GetBySecondaryItemId(long secondaryItemId, PagedRequest pagedRequest)
        {
            return await _unitOfWork.PrimaryItems.GetAllWithSecondaryItemBySecondaryItemIdAsync(secondaryItemId, pagedRequest);
        }

        public async Task<PrimaryItem> GetById(long id)
        {
            // We are chosing to return our item WITH our secondary item.
            return await _unitOfWork.PrimaryItems.GetWithSecondaryItemByIdAsync(id);
        }

        public async Task<PrimaryItem> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return await _unitOfWork.PrimaryItems.SingleOrDefaultAsync(x => x.Name.Equals(name));
        }

        public async Task Update(PrimaryItem originalItem, PrimaryItem updatedItem)
        {
            // Example of valdiating foreign key constraint
            if (originalItem.SecondaryItemId != updatedItem.SecondaryItemId && await _unitOfWork.SecondaryItems.GetByIdAsync(updatedItem.SecondaryItemId) == null)
            {
                throw new ArgumentNotFoundException(nameof(PrimaryItem.SecondaryItemId));
            }

            // Example of validating: Name is unique, we could catch exception too
            if (await _unitOfWork.PrimaryItems.SingleOrDefaultAsync(x => x.Name.Equals(updatedItem.Name) && x.Id != originalItem.Id) != null)
            {
                throw new ArgumentNotUniqueException(nameof(PrimaryItem.Name));
            }

            originalItem.Name = updatedItem.Name;
            originalItem.SecondaryItemId = updatedItem.SecondaryItemId;
            await _unitOfWork.CommitAsync();
        }
    }
}
