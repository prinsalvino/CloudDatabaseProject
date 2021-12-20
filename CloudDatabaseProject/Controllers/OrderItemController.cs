using AutoMapper;
using CloudDatabaseProject.DTO;
using CloudDatabaseProject.Infrastructure;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Service.ServiceInterfaces;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloudDatabaseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        IOrderItemService orderItemService;
        IOrderService orderService;
        IUserService userService;
        IMapper _mapper;
        ILogger logger;
        public OrderItemController(IOrderItemService orderItemService, IOrderService orderService, IUserService userService, IMapper mapper, ILogger logger)
        {
            this.orderItemService = orderItemService;
            this.orderService = orderService;
            this.userService = userService;
            _mapper = mapper;
            this.logger = logger;
        }
        // GET: api/<OrderItemController>
        [HttpGet]
        public IEnumerable<OrderItemReturnDTO> Get()
        {
            try
            {
                var newOrders = new List<OrderItemReturnDTO>();
                var orders = orderItemService.GetOrderItems();
                foreach (var order in orders)
                {
                    var newOrder = _mapper.Map<OrderItemReturnDTO>(order);
                    newOrders.Add(newOrder);
                }
                return newOrders;


            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        // GET api/<OrderItemController>/5
        [HttpGet("{id}")]
        public OrderItemReturnDTO Get(int id)
        {
            try
            {
                var order = orderItemService.GetOrderItem(id);
                var newOrder = _mapper.Map<OrderItemReturnDTO>(order);
                return newOrder;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        // POST api/<OrderItemController>
        [HttpPost]
        public void Post([FromBody] OrderItemDTO orderItemDTO)
        {
            try
            {
                User currentUser = userService.GetUser(1);
                Order order = orderService.getByStatusAndUserId(OrderStatus.Active, currentUser.Id);
                if (order == null)
                {
                    order = new Order(currentUser.Id, OrderStatus.Active);
                    orderService.AddOrder(order);
                    order = orderService.getByStatusAndUserId(OrderStatus.Active, currentUser.Id);
                }
                var orderItem = _mapper.Map<OrderItem>(orderItemDTO);
                orderItem.OrderId = order.OrderId;
                orderItemService.AddOrderItem(orderItem);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }           
        }

        // PUT api/<OrderItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] OrderItemDTO value)
        {
            try
            {
                OrderItem item = orderItemService.GetOrderItem(id);
                item.NumberOfItems = value.NumberOfItems;
                item.ProductId = value.ProductId;
                orderItemService.UpdateOrderItem(item);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
           
        }

        // DELETE api/<OrderItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                orderItemService.DeleteOrderItem(id);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }
    }
}
