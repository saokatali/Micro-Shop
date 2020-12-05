using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroShopMobile.Services
{
    public class DataService<T> : IDataService<T>
    {
        public Task<bool> AddItemAsync(T item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(T item)
        {
            throw new NotImplementedException();
        }
    }
}
