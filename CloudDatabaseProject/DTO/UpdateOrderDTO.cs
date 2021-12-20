using Domain;
using Newtonsoft.Json;
using System;

namespace CloudDatabaseProject.DTO
{
    public class UpdateOrderDTO
    {
        public OrderStatus Status { get; set; }
    }
}
