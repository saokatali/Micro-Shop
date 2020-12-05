using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Basket.API.Infrastructure
{
    public class CacheContext : ICacheContext
    {
        private readonly ILogger<CacheContext> logger;

        public CacheContext(IDistributedCache cache, ILogger<CacheContext> logger)
        {
            Cache = cache;
            this.logger = logger;
        }

        public IDistributedCache Cache { get; }

        public async Task<TItem> GetAsync<TItem>(string key)
        {
            var data = await Cache.GetAsync(key);
            if (data == null)
                return default;
            var json = Encoding.UTF8.GetString(data);
            return JsonSerializer.Deserialize<TItem>(json);


        }

        public async Task<bool> SetAsync<TItem>(string key, TItem item)
        {
            try
            {
                await Cache.SetAsync(key, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(item)));
                return true;
            }

            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return false;

            }
        }
    }
}
