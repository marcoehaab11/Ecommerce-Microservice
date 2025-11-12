using Microsoft.EntityFrameworkCore;
using Ordering.Core.Common;
using Ordering.Core.Reposiotory;
using Ordering.Infrastruture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastruture.Repositories
{
    public class OrderRepository : RepositoryBase<Order>,IOrderRepository
    {
        public OrderRepository(OrderDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserNameAsync(string userName)
        {
            var orderList = await _db.Orders
                .Where(o => o.UserName == userName)
                .ToListAsync();
            return orderList;
        }
    }
}
