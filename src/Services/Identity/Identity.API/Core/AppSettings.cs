namespace Identity.API.Core
{
    public class AppSettings
    {
        public SqlServerOptions SqlServer { get; set; }

        public LoggingOptions Logging { get; set; }

        public JWTOptions JWT { get; set; }

    }

    public class SqlServerOptions
    {
        public string ConnectionStrings { get; set; }


    }

    public class LoggingOptions
    {
        public string Provider { get; set; }
        public bool IsEnabled { get; set; }
    }



    public class JWTOptions
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public int ExpireDays { get; set; }
    }




}
