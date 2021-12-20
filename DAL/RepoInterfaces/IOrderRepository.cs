using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.RepoInterfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order GetOrderByStatusAndUserId(OrderStatus orderStatus, int id);
    }
}
