using Microsoft.EntityFrameworkCore;
using Ordering.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastruture.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions options) : base(options){}
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries<EntityBase>())
            {
                switch(item.State)
                {
                    case EntityState.Added:
                        item.Entity.CreateDate = DateTime.UtcNow;
                        item.Entity.CreateBy="Marco"; //TODO :: Replace with actual user
                        break;
                    case EntityState.Modified:
                        item.Entity.UpdateDate = DateTime.UtcNow;
                        item.Entity.UpdateBy = "Marco"; //TODO :: Replace with actual user
                        break;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        
       public DbSet<Order> Orders { get; set; }
        
    
    
    }
}
