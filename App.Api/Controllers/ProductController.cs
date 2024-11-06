using App.BL;
using App.BL.IServices;
using App.BL.Models;
using App.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PaginationResult<ProductDto>>>> GetAll([FromQuery] PaginationFilter paginationFilter)
        {
            var response = await _productService.GetAll(paginationFilter);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
