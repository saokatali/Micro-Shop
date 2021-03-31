using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Common.MessageBrokers.Clients
{
    public class RabbitMQClient : IBusClient, IDisposable
    {

        private IConnection connection;
        private IModel channel;
        string exchange;
        string queue;
        string routingKey;


      

      ~ RabbitMQClient()
        {
            Close();
        }

        public void CreateConnection(string hostName, string userName, string password)
        {

            try
            {
                var cf = new ConnectionFactory();
                cf.HostName = hostName;
                cf.UserName = userName;
                cf.Password = password;
                connection = cf.CreateConnection();
                channel = connection.CreateModel();
               
            }
            catch
            {
                
            }

        }



        public void Close()
        {
            try
            {
                connection.Close();
                channel.Close();
            }
            catch
            {
                
            }


        }


        public void Publish<T>(T data) 
        {

            channel.BasicPublish(exchange, routingKey, null, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data)));
        }
        public void Subscribe(EventHandler<BasicDeliverEventArgs> callback)
        {

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += callback;


            channel.BasicConsume(queue, false, consumer);
        }

        public void CreateConnection()
        {
            throw new NotImplementedException();
        }

        public void Bind(string exchange, string queue, string routingKey, string exchangType = "direct")
        {
            this.exchange = exchange;
            this.queue = queue;
            this.routingKey = routingKey;
            channel.ExchangeDeclare(exchange,exchangType, true, false);
            channel.QueueDeclare(queue, true, false, false);
            channel.QueueBind(queue, exchange, routingKey);
        }

        public void Dispose()
        {
            Close();
        }
    }
}
