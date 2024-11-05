using System.ComponentModel.DataAnnotations;

namespace App.BL.DTOs
{
    public class OrderDto
    {
        [Required(ErrorMessage = "Order items are required.")]
        [MinLength(1, ErrorMessage = "At least one order item must be provided.")]
        public List<OrderItemDto> Items { get; set; }
    }

    public class OrderItemDto
    {
        [Required(ErrorMessage = "Product ID is required.")]
        [Range(1, 10, ErrorMessage = "Product ID must be greater than 0.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 10, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }
    }


}
