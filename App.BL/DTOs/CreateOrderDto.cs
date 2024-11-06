using System.ComponentModel.DataAnnotations;

namespace App.BL
{
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "Order items are required.")]
        [MinLength(1, ErrorMessage = "At least one order item must be provided.")]
        public List<CreateOrderItemDto> OrderItems { get; set; }
        
    }



    public class CreateOrderItemDto
    {
        [Required(ErrorMessage = "Product ID is required.")]
        [Range(1, 100, ErrorMessage = "Product ID must be greater than 0.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 100, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }

       
    }



}
