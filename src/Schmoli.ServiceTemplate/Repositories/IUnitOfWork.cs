using System;
using System.Threading.Tasks;
using Schmoli.ServiceTemplate.Repositories;

namespace Schmoli.ServiceTemplate
{
    public interface IUnitOfWork : IDisposable
    {
        IPrimaryItemRepository PrimaryItems { get; }
        ISecondaryItemRepository SecondaryItems { get; }
        Task<int> CommitAsync();
    }
}
