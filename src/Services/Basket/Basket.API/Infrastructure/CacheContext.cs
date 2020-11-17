using Microsoft.Extensions.Caching.Distributed;
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
        public CacheContext(IDistributedCache cache)
        {
            Cache = cache;
        }

        public IDistributedCache Cache { get; }

        public Task<TItem> GetAsync<TItem>(string key)
        {
            throw new NotImplementedException();
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
                return false;

            }
        }
    }
}
