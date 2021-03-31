using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace Common.MessageBrokers
{
    public interface IBusClient
    {
        void CreateConnection(string hostName, string userName, string password);

        void Close();

        void Publish<T>(T data); 

        void Subscribe(EventHandler<BasicDeliverEventArgs> callback);

        void Bind(string exchange, string queue, string routingKey, string exchangType = "direct");

    }
}
