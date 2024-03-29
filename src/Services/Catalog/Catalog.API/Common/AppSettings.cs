﻿namespace Catalog.API.Common
{
    public class AppSettings
    {
        public SqlServer SqlServer { get; set; }

        public Logging Logging { get; set; }

        public bool IsTest { get; set; }

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
