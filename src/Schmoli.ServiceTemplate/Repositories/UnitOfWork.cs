using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Schmoli.ServiceTemplate.Data;

namespace Schmoli.ServiceTemplate.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ServiceDbContext _context;
        private PrimaryItemRepository _primaryItemrepository;
        private SecondaryItemRepository _secondaryitemRepository;

        public UnitOfWork(ServiceDbContext context)
        {
            _context = context;
        }

        public IPrimaryItemRepository PrimaryItems => _primaryItemrepository ??= new PrimaryItemRepository(_context);

        public ISecondaryItemRepository SecondaryItems => _secondaryitemRepository ??= new SecondaryItemRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
