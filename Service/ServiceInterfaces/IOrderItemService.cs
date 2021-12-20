using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces
{
    public interface IOrderItemService
    {
        public void AddOrderItem(OrderItem useitemr);

        public void UpdateOrderItem(OrderItem user);

        public OrderItem GetOrderItem(int id);

        public void DeleteOrderItem(int id);

        public IEnumerable<OrderItem> GetOrderItems();
    }
}
