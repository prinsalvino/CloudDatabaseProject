using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Order
    {
        [Key]
        public int OrderId {  get; set; }

        public int UserId { get;set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public OrderStatus Status { get; set; }

        [JsonIgnore]
        public List<OrderItem> Items {  get; set;} = new List<OrderItem>();

        public Order(int userId, OrderStatus status)
        {
            UserId = userId;
            Status = status;
        }

        public Order()
        {

        }
    }
}
