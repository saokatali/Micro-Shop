using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Ordering.API.Common;
using Ordering.API.Domain.Models.Entities;
using System.Reflection;

namespace Ordering.API.Infrastructure
{
    public class DataContext : DbContext
    {
        private readonly AppSettings appSettings;

        const string SqlServerProvider = "SqlServer";
        const string PostgresProvider = "Postgres";
        const string InMemoryProvider = "InMemory";

        public DataContext(IOptionsMonitor<AppSettings> options)
        {
            this.appSettings = options.CurrentValue;
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Shipping> Shippings { get; set; }

        #region overrides

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
                {
                    var entity = entry.Entity as EntityBase;
                    if (entity != null)
                    {
                        entity.UpdatedDate = DateTime.UtcNow;
                        if (entry.State == EntityState.Added)
                        {
                            entity.CreatedDate = DateTime.UtcNow;
                        }

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            switch (appSettings.Database.Provider)
            {
                case SqlServerProvider:
                    optionsBuilder.UseSqlServer(appSettings.SqlServer.ConnectionStrings);
                    break;
                case PostgresProvider:
                    optionsBuilder.UseSqlServer(appSettings.Postgres.ConnectionStrings);
                    break;
                case InMemoryProvider:
                    optionsBuilder.UseInMemoryDatabase("Db");
                    break;

            }
           
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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

        private static readonly MethodInfo SetGlobalQueryMethod = typeof(DataContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                                       .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQuery");

        public void SetGlobalQuery<T>(ModelBuilder builder) where T : EntityBase
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        #endregion overrides
    }
}