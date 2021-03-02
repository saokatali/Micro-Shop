using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;

namespace MicroShopMobile.Services
{
    public class DataService<T> : IDataService<T>
    {
        HttpClient httpClient;

        public DataService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<bool> AddItemAsync(T item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetItemAsync(Guid id)
        {
            return await httpClient.GetFromJsonAsync<T>(UrlHelper.Get<T>());
        }

        public async Task<IEnumerable<T>> GetItemsAsync()
        {
            return await httpClient.GetFromJsonAsync<List<T>>(UrlHelper.GetAll<T>());
        }

        public Task<bool> UpdateItemAsync(T item)
        {
            throw new NotImplementedException();
        }
    }
}
