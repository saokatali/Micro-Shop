using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.MessageBrokers.Clients;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Common.MessageBrokers.Extension
{
    public static class RabbitMQExtension
    {
        public static void AddRabbitMQ(this IServiceCollection services, Action<BrokerOption> action)
        {
           
            var option = new BrokerOption();
            action.Invoke(option);
            IBusClient client = new RabbitMQClient();
            client.CreateConnection(option.Host, option.UserName, option.Password);

            foreach (var bindOption in option.BindOptions)
            {
                client.Bind(bindOption.Exchange, bindOption.Queue, bindOption.RoutingKey);
            }
            services.AddSingleton<IBusClient>(client);
        }

        public static void UseRabbitMQ(this IApplicationBuilder app, Action<BrokerOption> action)
        {          
            var option = new BrokerOption();
            action.Invoke(option);
            IBusClient client = app.ApplicationServices.GetRequiredService<IBusClient>();
            client.CreateConnection(option.Host, option.UserName, option.Password);

            foreach (var bindOption in option.BindOptions)
            {
                client.Bind(bindOption.Exchange, bindOption.Queue, bindOption.RoutingKey);
            }

        }

    }
}
