using System;

namespace CloudDatabaseProject.DTO
{
    public class OrderDescriptionDTO
    {
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public string Review { get; set; }
    }
}
