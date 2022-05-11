using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Infrastracture.Persistence
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }

        public override int SaveChanges()
        {
            TrackChanges();

            return base.SaveChanges();
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            TrackChanges();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void TrackChanges()
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                DateTime time = DateTime.Now;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = time; 
                        entry.Entity.LastModifiedBy = "cul"; 
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = time; 
                        entry.Entity.LastModifiedBy = "cul"; 
                        break;
                }
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
