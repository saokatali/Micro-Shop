using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Common
{
    public class AppSettings
    {
        public SqlServer SqlServer { get; set; }

        public Logging Logging { get; set; }

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
}
