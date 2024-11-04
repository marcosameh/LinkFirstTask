using App.BL.Common;
using App.BL.DTOs;
using App.BL.IRepository;
using App.DAL.Entities;
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
        public ActionResult<ApiResponse<Order>> Create([FromBody] OrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage);
                return BadRequest(new ApiResponse<Order>(false, string.Join("\n", errors), null));
            }

            var response = _orderRepository.CreateOrder(orderDto);

            if (response.Success)
            {
                return CreatedAtAction(nameof(Create), new { id = response.Data.Id }, response);
            }

            return BadRequest(response);
        }
    }
}
