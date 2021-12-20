using AutoMapper;
using Domain;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Service.ServiceInterfaces;

namespace QueueOrder
{
    public class OrderQueue
    {
        IOrderService orderService;
        IMapper _mapper;
     
        public OrderQueue(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this._mapper = mapper;
        }

        [FunctionName("OrderQueue")]
        public void Run([QueueTrigger("orderqueue", Connection = "AzureWebJobsStorage")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            var orderDTO = JsonConvert.DeserializeObject(myQueueItem);
            var order = _mapper.Map<Order>(orderDTO);
            orderService.AddOrder(order);
        }
    }
}
