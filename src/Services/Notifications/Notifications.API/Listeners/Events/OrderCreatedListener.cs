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

namespace Notifications.API.Listeners.Events
{
    public class OrderCreatedListener : IHostedService
    {
        IBusClient busClient;


        public OrderCreatedListener(IBusClient busClient)
        {
            this.busClient = busClient;
        }


            public Task StartAsync(CancellationToken cancellationToken)
            {


                busClient.Subscribe( (obj, e) =>
                {
                    OrderDto order = JsonSerializer.Deserialize<OrderDto>(Encoding.UTF8.GetString(e.Body.ToArray()));

                    ((EventingBasicConsumer)obj).Model.BasicAck(e.DeliveryTag, false);

                });

                return Task.CompletedTask;
            }

            public Task StopAsync(CancellationToken cancellationToken)
            {
                busClient.Close();
                return Task.CompletedTask;
            }

        
    }
}
