using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.MessageBrokers
{
    public class BindOption
    {
        public string Queue  { get; set; }
        public string Exchange { get; set; }

        public string RoutingKey { get; set; }

        public string ExchangeType { get; set; }


    }
}
