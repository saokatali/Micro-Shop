using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Notifications.API.Dtos;

namespace Notifications.API.Infrastructure.ServiceClients
{
    public class OrderService:IOrderService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public OrderService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task<OrderDto> GetDetails(long orderId)
        {
            var orderServiceURL = configuration["ServiceURL:Order"] + $"?orderId={orderId}";
            return await httpClient.GetFromJsonAsync<OrderDto>(orderServiceURL);
            
        }
    }
}
