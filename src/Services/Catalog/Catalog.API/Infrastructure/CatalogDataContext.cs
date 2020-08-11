using Catalog.API.Core;
using Catalog.API.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.API.Infrastructure
{
    public class CatalogDataContext : DbContext
    {
        #region Entities
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }

        #endregion





        #region Configuration
        public AppSettings AppSettings { get; }
        public CatalogDataContext(IOptionsSnapshot<AppSettings> appSettings)
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

            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = type.ClrType;


                if (clrType.BaseType.IsClass && clrType.BaseType.Name.Contains("BaseEntity"))
                {
                    var method = SetGlobalQueryMethod.MakeGenericMethod(clrType);
                    method.Invoke(this, new object[] { modelBuilder });
                }


            }

        }



        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Deleted || entry.State == EntityState.Deleted)
                {
                    var entity = entry.Entity as BaseEntity;
                    if (entity != null)
                    {
                        entity.UpdatedDate = DateTime.UtcNow;


                        if (entry.State == EntityState.Deleted)
                        {
                            entry.State = EntityState.Modified;
                            entity.IsDeleted = true;
                        }
                    }

                }

            }

            return base.SaveChangesAsync(cancellationToken);
        }

        static readonly MethodInfo SetGlobalQueryMethod = typeof(CatalogDataContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                                        .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQuery");

        public void SetGlobalQuery<T>(ModelBuilder builder) where T : BaseEntity
        {

            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }



        #endregion
    }
}
