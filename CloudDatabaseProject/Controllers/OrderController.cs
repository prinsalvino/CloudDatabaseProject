using AutoMapper;
using CloudDatabaseProject.DTO;
using CloudDatabaseProject.Infrastructure;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderQueue;
using Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloudDatabaseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderService orderService;
        IMapper _mapper;
        IQueueStorage queueStorage;
        IUserService userService;
        ITableStorage tableStorage;
        ILogger logger;
        public OrderController(IOrderService orderService, IMapper mapper, IQueueStorage queueStorage, IUserService userService, ITableStorage tableStorage, ILogger logger)
        {
            this.orderService = orderService;
            this._mapper = mapper;
            this.queueStorage = queueStorage;
            this.userService = userService;
            this.tableStorage = tableStorage;
            this.logger = logger;
        }
        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<OrderReturnDTO> Get()
        {
            try
            {
                var newOrders = new List<OrderReturnDTO>();
                var orders = orderService.GetOrders();
                foreach (var order in orders)
                {
                    var newOrder = _mapper.Map<OrderReturnDTO>(order);
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

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public OrderReturnDTO Get(int id)
        {
            try
            {
                var order = orderService.GetOrder(id);
                var newOrder = _mapper.Map<OrderReturnDTO>(order);
                return newOrder;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;

            }
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task Post([FromBody] OrderDTO orderDTO)
        {
            try
            {
                User currentUser = userService.GetUser(1);
                Order order = orderService.getByStatusAndUserId(OrderStatus.Active, currentUser.Id);
                if (order == null)
                {
                    order = new Order(currentUser.Id, OrderStatus.Active);

                    // Send a message to queue storage
                    string jsonString = JsonConvert.SerializeObject(order);
                    await queueStorage.CreateMessage(jsonString);
                    OrderDescription orderDescription = new OrderDescription(order.OrderId.ToString(), DateTime.Now, DateTime.Now);
                    await tableStorage.InsertRecordToTable(orderDescription);
                }
               
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UpdateOrderDTO value)
        {
            try
            {
                var newOrder = _mapper.Map<Order>(value);
                var existOrder = orderService.GetOrder(id);

                existOrder.Status = newOrder.Status;
                orderService.UpdateOrder(existOrder);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                orderService.DeleteOrder(id);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }

        // Add review api/<OrderController>/5
        [HttpPut("{id}/review")]
        public async void AddReview(int id, OrderReviewDTO orderReview)
        {
            try
            {
                await tableStorage.UpdateRecordInTable(id, orderReview.Review);

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }

        // Add review api/<OrderController>/5
        [HttpGet("reviews")]
        public List<OrderDescriptionDTO> GetAllReview()
        {
            try
            {
                List<OrderDescriptionDTO> orderDescriptionDTOs = new List<OrderDescriptionDTO>();
                var orders = tableStorage.GetAllOrders();
                foreach (var order in orders)
                {
                    var newOrder = _mapper.Map<OrderDescriptionDTO>(order);
                    orderDescriptionDTOs.Add(newOrder);
                }
                return orderDescriptionDTOs;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        // Add review api/<OrderController>/5
        [HttpGet("reviews/{id}")]
        public List<OrderDescriptionDTO> GetReviewById(int id)
        {
            try
            {
                List<OrderDescriptionDTO> orderDescriptionDTOs = new List<OrderDescriptionDTO>();
                var orders = tableStorage.GetOrderById(id);
                foreach (var order in orders)
                {
                    var newOrder = _mapper.Map<OrderDescriptionDTO>(order);
                    orderDescriptionDTOs.Add(newOrder);
                }
                return orderDescriptionDTOs;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

    }
}
