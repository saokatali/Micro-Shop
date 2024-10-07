using Common.Web.Middleware;
using Identity.API.Core;
using Identity.API.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.AddServerHeader = false;
});

// Add services to the DI container.
var services = builder.Services;
services.AddOptions();
services.Configure<AppSettings>(builder.Configuration);
services.AddDbContext<AppDbContext>(ServiceLifetime.Transient);
services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();
const string defaultCors = "DefaultCorsPolicy";
services.AddCors(options =>
{
    options.AddPolicy(defaultCors, policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity API", Version = "v1" }));
services.AddControllers();

var app = builder.Build();

app.UseException();
if (!app.Environment.IsDevelopment())
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
app.UseCors(defaultCors);

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

var applicationLifetime = app.Lifetime;

applicationLifetime.ApplicationStarted.Register(() =>
{
});

applicationLifetime.ApplicationStopped.Register(() =>
{
});
app.Run();