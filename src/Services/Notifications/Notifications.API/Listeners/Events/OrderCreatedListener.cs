using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Common.MessageBrokers.Clients;
using Common.MessageBrokers;
using RabbitMQ.Client.Events;
using Notifications.API.Dtos;
using System.Text.Json;
using System.Text;
using Notifications.API.Infrastructure.ServiceClients;

namespace Notifications.API.Listeners.Events
{
    public class OrderCreatedListener : IHostedService
    {
        private readonly IBusClient busClient;
        private readonly IOrderService orderService;

        public OrderCreatedListener(IBusClient busClient, IOrderService orderService)
        {
            this.busClient = busClient;
            this.orderService = orderService;
        }


            public async Task StartAsync(CancellationToken cancellationToken)
            {


                busClient.Subscribe( async (obj, e) =>
                {
                    OrderCreatedData order = JsonSerializer.Deserialize<OrderCreatedData>(Encoding.UTF8.GetString(e.Body.ToArray()));
                    var orderDetails = await orderService.GetDetails(order.OrderId);


                    ((EventingBasicConsumer)obj).Model.BasicAck(e.DeliveryTag, false);

                });


            }

            public Task StopAsync(CancellationToken cancellationToken)
            {
                busClient.Close();
                return Task.CompletedTask;
            }

        
    }
}
