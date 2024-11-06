using System.ComponentModel.DataAnnotations;

namespace App.BL.DTOs
{
    public class SubmitOrderDto
    {
        [Required(ErrorMessage = "Order id is required.")]
        public int OrderId { get; set; }
        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100, ErrorMessage = "Customer name can't exceed 100 characters.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Customer phone is required.")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Customer phone must be a valid Egyptian number.")]
        public string CustomerPhone { get; set; }

        [Required(ErrorMessage = "Customer address is required.")]
        [StringLength(250, ErrorMessage = "Customer address can't exceed 250 characters.")]
        public string CustomerAddress { get; set; }
    }
}
