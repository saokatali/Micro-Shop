using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Catalog.API.Domain.Models.Entities;
using Microsoft.Extensions.Options;
using Catalog.API.Core;

namespace Catalog.API.Infrastructure
{
    public class CatalogDbContext : DbContext
    {
        public AppSettings AppSettings { get; }
        public CatalogDbContext(IOptionsSnapshot<AppSettings> appSettings)
        {
            AppSettings = appSettings.Value;
        }

    

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(AppSettings.SqlServer.ConnectionStrings);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CategoryProduct>().HasKey(e => new { e.CategoryId, e.ProductId });
        }
    }
}
