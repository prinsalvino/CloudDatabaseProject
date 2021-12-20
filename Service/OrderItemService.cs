using DAL.RepoInterfaces;
using Domain;
using Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderItemService : IOrderItemService
    {
        IOrderItemRepository _repository;
        public OrderItemService(IOrderItemRepository repository)
        {
            _repository = repository;
        }

        public void AddOrderItem(OrderItem useitem)
        {
            _repository.Add(useitem);
        }

        public void DeleteOrderItem(int id)
        {
            _repository.Delete(_repository.GetSingle(id));
        }

        public OrderItem GetOrderItem(int id)
        {
            return _repository.GetSingle(id);
        }

        public IEnumerable<OrderItem> GetOrderItems()
        {
            return _repository.GetAll();
        }

        public void UpdateOrderItem(OrderItem item)
        {
            _repository.Update(item);
        }
    }
}
