using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Identity.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Simplifies Code 
            IHostBuilder hostBuilder = CreateHostBuilder(args);
            IHost host = hostBuilder.Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel(options =>
                    {
                        options.AddServerHeader = false;

                    });
                });
    }
}
