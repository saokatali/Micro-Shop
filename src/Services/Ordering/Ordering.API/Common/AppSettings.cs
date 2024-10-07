namespace Ordering.API.Common
{
    public class AppSettings
    {
        public SqlServerOptions SqlServer { get; set; }

        public PostgresOPtions Postgres { get; set; }

        public LoggingOptions Logging { get; set; }

        public DatabaseOptions Database { get; set; }
    }


    public class DatabaseOptions
    {
        public string Provider { get; set; }
    }

    public class SqlServerOptions
    {
        public string ConnectionStrings { get; set; }
    }

    public class PostgresOPtions
    {
        public string ConnectionStrings { get; set; }
    }


    public class LoggingOptions
    {
        public string Provider { get; set; }
        public bool IsEnabled { get; set; }
    }
}