using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class OrderDescription : TableEntity
    {
        public string orderId;
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public string Review { get; set; }

        public OrderDescription()
        {

        }

        public OrderDescription(string orderId, DateTime orderDate, DateTime shippingDate)
        {
            this.orderId = orderId;
            OrderDate = orderDate;
            ShippingDate = shippingDate;

            PartitionKey = "OrderDescription";
            RowKey = orderId;
        }
    }
}
