using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Infrastructure
{
    public interface ICacheContext
    {
        Task<TItem> GetAsync<TItem>(string key);
        Task<bool> SetAsync<TItem>(string key, TItem item);
    }
}
