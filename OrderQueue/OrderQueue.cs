using System;
using AutoMapper;
using CloudDatabaseProject.Infrastructure;
using Domain;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Service;
using Service.ServiceInterfaces;

namespace OrderQueue
{
    public class OrderQueue
    {
        IOrderService orderService;
        public OrderQueue(IOrderService orderService, ITableStorage tableStorage)
        {
            this.orderService = orderService;
        }
        [Function("QueueTrigger")]
        public void Run([QueueTrigger("orderqueue", Connection = "AzureWebJobsStorage")] string myQueueItem,
            FunctionContext context)
        {
            var logger = context.GetLogger("QueueTrigger");
            logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

            Order order = JsonConvert.DeserializeObject<Order>(myQueueItem);
            orderService.AddOrder(order);
            logger.LogInformation("Success");
        }
    }
}
