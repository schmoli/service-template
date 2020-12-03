using Schmoli.Services.Core.Repositories;
using Schmoli.ServiceTemplate.Data;
using Schmoli.ServiceTemplate.Models;

namespace Schmoli.ServiceTemplate.Repositories
{
    public class SecondaryItemRepository : Repository<SecondaryItem>, ISecondaryItemRepository
    {
        public SecondaryItemRepository(ServiceDbContext context)
            : base(context) { }
    }
}
