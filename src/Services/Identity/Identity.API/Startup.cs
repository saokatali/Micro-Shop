using Common.Web.Middleware;
using Identity.API.Core;
using Identity.API.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;




namespace Identity.API
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
            services.AddOptions();
            services.Configure<AppSettings>(Configuration);
            services.AddDbContext<AppDbContext>(ServiceLifetime.Scoped);
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("DefaultCors", policy => policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

            });
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity API", Version = "v1" }));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {


            app.UseException();
            if (!env.IsDevelopment())
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog APICatalog API");

            });

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            applicationLifetime.ApplicationStopped.Register(() =>
            {

            });


        }
    }
}
