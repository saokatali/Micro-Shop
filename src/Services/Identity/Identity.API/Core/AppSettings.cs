using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Core
{
    public class AppSettings
    {
        public SqlServer SqlServer { get; set; }

        public MongoDBSettings MongoDB { get; set; }

        public Logging Logging { get; set; }

        public JWT JWT { get; set; }
        public RedisSettings Redis { get; set; }

        public RabbitMQSettings RabbitMQ { get; set; }

    }

    public class SqlServer
    {
        public string ConnectionStrings { get; set; }


    }

    public class Logging
    {
        public string Provider { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class MongoDBSettings
    {
        public string ConnectionStrings { get; set; }

        public string Db { get; set; }
    }

    public class JWT
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public int ExpireDays { get; set; }
    }

    public class RedisSettings
    {
        public string Configuration { get; set; }

        public string InstanceName { get; set; }

    }

    public class RabbitMQSettings
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string VHost { get; set; }
        public string User { get; set; }

        public string Pass { get; set; }

        public string Exchange { get; set; }

        public string ExchangeType { get; set; }

        public string Queue { get; set; }

        public string RoutingKey { get; set; }

    }
}
