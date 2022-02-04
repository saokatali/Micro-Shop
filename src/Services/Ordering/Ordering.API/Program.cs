using Common.Web.Middleware;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ordering.API.Common;
using Ordering.API.Infrastructure;
using Ordering.API.Policies;
using System;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IServiceCollection services = builder.Services;

services.AddOptions();
services.Configure<AppSettings>(builder.Configuration);
services.AddHttpContextAccessor();
services.AddDbContext<DataContext>(ServiceLifetime.Transient);
services.AddMediatR(Assembly.GetExecutingAssembly());
services.AddAutoMapper(Assembly.GetExecutingAssembly());
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opions =>
{
    opions.RequireHttpsMetadata = true;
    opions.SaveToken = true;
    opions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
    };
});
services.AddAuthorization(options =>
{
    options.AddPolicy("AtLeast18", policy =>
    {
        policy.Requirements.Add(new MinimumAgeRequirement(18));
    });

    options.AddPolicy("AdminRole", policy =>
    {
        policy.RequireClaim("Role", "Admin");
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