using Schmoli.ServiceTemplate.Data;
using Schmoli.ServiceTemplate.Models;
using Schmoli.Services.Core.Repositories;

namespace Schmoli.ServiceTemplate.Repositories
{
    public class SecondaryItemRepository : Repository<SecondaryItem>, ISecondaryItemRepository
    {
        public SecondaryItemRepository(ServiceDbContext context)
            : base(context) { }
    }
}
