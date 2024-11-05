using System.ComponentModel.DataAnnotations;

namespace App.BL.DTOs
{
    public class OrderDto
    {
        [Required(ErrorMessage = "Order items are required.")]
        [MinLength(1, ErrorMessage = "At least one order item must be provided.")]
        public List<OrderItemDto> Items { get; set; }
        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100, ErrorMessage = "Customer name can't exceed 100 characters.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Customer phone is required.")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Customer phone must be a valid Egyptian number.")]
        public string CustomerPhone { get; set; }

        [Required(ErrorMessage = "Customer address is required.")]
        [StringLength(200, ErrorMessage = "Customer address can't exceed 200 characters.")]
        public string CustomerAddress { get; set; }
    }



    public class OrderItemDto
    {
        [Required(ErrorMessage = "Product ID is required.")]
        [Range(1, 100, ErrorMessage = "Product ID must be greater than 0.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 100, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }

       
    }



}
