using App.BL.Common;
using App.BL.DTOs;
using App.BL.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<OrderDto>>> Create([FromBody] OrderDto orderDto)
        {


            var response =await _orderRepository.CreateOrderAsync(orderDto);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
