namespace App.BL.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
