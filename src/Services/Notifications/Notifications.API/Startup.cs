using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Common.MessageBrokers.Extension;
using Common.MessageBrokers;
using Notifications.API.Listeners.Events;

namespace Notifications.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddRabbitMQ(option => {
                option.Host = Configuration["RabbitMQ:Host"];
                option.UserName = Configuration["RabbitMQ:UserName"];
                option.Password = Configuration["RabbitMQ:Password"];
                option.BindOptions.Add(new BindOption { Exchange = Configuration["RabbitMQ:Exchange"], Queue = Configuration["RabbitMQ:Queue"], ExchangeType = Configuration["RabbitMQ:ExchangeType"], RoutingKey = Configuration["RabbitMQ:RoutingKey"] });
            });
            services.AddHostedService<OrderCreatedListener>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

          

        }
    }
}
