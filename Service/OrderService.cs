using DAL.RepoInterfaces;
using Domain;
using Microsoft.Extensions.Logging;
using Service.ServiceInterfaces;
using System;
using System.Collections.Generic;

namespace Service
{
    public class OrderService : IOrderService
    {
        IOrderRepository repository;
        ILogger logger;
        public OrderService(IOrderRepository repository, ILogger logger)
        {
            this.repository = repository; 
            this.logger = logger;
        }

        public void AddOrder(Order order)
        {
            repository.Add(order);
        }

        public void DeleteOrder(int id)
        {
            Order order = repository.GetSingle(id);
            repository.Delete(order);
        }

        public Order GetOrder(int id)
        {
            try
            {
                return repository.GetSingle(id);

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public IEnumerable<Order> GetOrders()
        {
            return repository.GetAll();
        }

        public void UpdateOrder(Order item)
        {
            repository.Update(item);
        }

        public Order getByStatusAndUserId(OrderStatus orderStatus, int userId)
        {
            Order order = repository.GetOrderByStatusAndUserId(orderStatus, userId);
            if(order == null)
                return null;
            return repository.GetOrderByStatusAndUserId(orderStatus, userId);
        }
    }
}
