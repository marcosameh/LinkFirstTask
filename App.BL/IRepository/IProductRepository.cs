using App.BL.Common;
using App.BL.DTOs;
using App.BL.Models;

namespace App.BL.IRepository
{
    internal interface IProductRepository
    {
        public Task<ApiResponse<PaginationResult<ProductDto>>> GetAll(PaginationFilter paginationFilter);
    }
}
