using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Schmoli.Services.Core.Cache
{
    public class DistributedServiceCache : IServiceCache
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<DistributedServiceCache> _logger;
        private readonly DistributedCacheEntryOptions _options;

        public DistributedServiceCache(
            IDistributedCache cache,
            DistributedCacheEntryOptions options,
            ILogger<DistributedServiceCache> logger)
        {
            _cache = cache;
            _logger = logger;
            _options = options;
        }

        /// <summary>
        /// Add item to cache
        /// </summary>
        /// <param name="key">unqiue key - could be item type + id</param>
        /// <param name="item">object to cache</param>
        /// <returns></returns>
        public async Task AddAsync(string key, object item)
        {
            var serializedItem = JsonSerializer.Serialize(item);
            _logger.LogDebug($"CACHE ADD /{key} [{serializedItem.Length}]");
            await _cache.SetStringAsync(key, serializedItem, _options);
        }

        /// <summary>
        /// Get item from distributed cache
        /// </summary>
        public async Task<T> GetAsync<T>(string key)
        {
            T item = default;
            var serializedItem = await _cache.GetStringAsync(key);
            if (serializedItem != null)
            {
                _logger.LogDebug($"CACHE GET /{key} [{serializedItem.Length}]");
                item = JsonSerializer.Deserialize<T>(serializedItem);
            }

            return item;
        }

        /// <summary>
        /// Remove item from cache
        /// </summary>
        public async Task RemoveAsync(string key)
        {
            _logger.LogDebug($"CACHE DEL /{key}");
            await _cache.RemoveAsync(key);
        }
    }
}
