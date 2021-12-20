using DAL.RepoInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        ShoppingContext shopContext;
        public OrderRepository(ShoppingContext context) : base(context)
        {
            this.shopContext = context;
        }

        public Order GetOrderByStatusAndUserId(OrderStatus orderStatus, int userId)
        {
           var order = shopContext.Set<Order>().Where(x=> x.Status == orderStatus && x.UserId == userId).FirstOrDefault();
           return order;
        }
    }
}
