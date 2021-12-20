using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces
{
    public interface IOrderService
    {
        public void AddOrder(Order order);

        public void UpdateOrder(Order user);

        public Order GetOrder(int id);

        public void DeleteOrder(int id);

        public IEnumerable<Order> GetOrders();
        public Order getByStatusAndUserId(OrderStatus orderStatus, int userId);

    }
}
