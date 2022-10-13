using Microsoft.EntityFrameworkCore;
using ordering.domain.Common;
using ordering.domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ordering.infrastructure.Persistence
{
    public class OrderContext :  DbContext
    {

        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {

        }


        public DbSet<Order> Orders { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch(entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "swn";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "swn";
                        break;
                }
            }


            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
