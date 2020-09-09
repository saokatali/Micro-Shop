using Identity.API.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace Identity.API.Infrastructure
{
    public class AppDbContext:IdentityDbContext<AppUser,AppRole,Guid>
    {
        private readonly AppSettings appSettings;
        public AppDbContext(IOptionsSnapshot<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(appSettings.SqlServer.ConnectionStrings);
        }

    }
}
