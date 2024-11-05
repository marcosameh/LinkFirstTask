using App.BL.Common;
using App.BL.DTOs;
using App.BL.IRepository;
using App.BL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PaginationResult<ProductDto>>>> GetAll([FromQuery] PaginationFilter paginationFilter)
        {
            var response = await _productRepository.GetAll(paginationFilter);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
