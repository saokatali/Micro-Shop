using Common.Web.Middleware;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ordering.API.Common;
using Ordering.API.Infrastructure;
using Ordering.API.Policies;
using System.Reflection;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IServiceCollection services = builder.Services;
IConfiguration configuration = builder.Configuration;

services.AddOptions();
services.Configure<AppSettings>(configuration);
services.AddHttpContextAccessor();
services.AddDbContext<DataContext>(ServiceLifetime.Transient);
services.AddMediatR(Assembly.GetExecutingAssembly());
services.AddAutoMapper(Assembly.GetExecutingAssembly());

services.AddHttpClient("ProductService", client =>
{
    client.BaseAddress = new Uri(configuration["ProductServiceURL"]);
}
);
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opions =>
{
    opions.RequireHttpsMetadata = true;
    opions.SaveToken = true;
    opions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]))
    };
});
services.AddAuthorization(options =>
{
    options.AddPolicy("AdminRole", policy =>
    {
        policy.RequireClaim("Role", "Admin");
    });
    options.AddPolicy("AtLeast18", policy =>
    {
        policy.Requirements.Add(new AgeRequirement(20, 40));
    });
});

services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});

services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Order API", Version = "v1" }));

// Configure the HTTP request pipeline.

var app = builder.Build();
var env = app.Environment;
app.UseException();

if (!env.IsDevelopment())
{
    app.UseHsts();
    app.UseHttpsRedirection();
}
if (env.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DefaultModelsExpandDepth(-1);
        c.RoutePrefix = string.Empty;
        // string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order API");
    });
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();