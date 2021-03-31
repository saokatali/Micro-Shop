using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.MessageBrokers
{
    public class BrokerOption
    {
        public string Host { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string VirtualHost { get; set; }

        public List<BindOption> BindOptions { get; set; } = new List<BindOption>();

    }
}
