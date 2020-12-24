using System.Threading.Tasks;

namespace Schmoli.Services.Core.Cache
{
    public interface IServiceCache
    {
        Task AddAsync(string key, object item);
        Task<T> GetAsync<T>(string key);
        Task RemoveAsync(string key);
    }

}
