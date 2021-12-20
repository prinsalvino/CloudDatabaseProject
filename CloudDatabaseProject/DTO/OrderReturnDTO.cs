using Domain;

namespace CloudDatabaseProject.DTO
{
    public class OrderReturnDTO
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public OrderStatus Status { get; set; }
    }
}
