namespace CloudDatabaseProject.DTO
{
    public class OrderItemReturnDTO
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
       
        public int ProductId { get; set; }
       
        public int NumberOfItems { get; set; }
    }
}
