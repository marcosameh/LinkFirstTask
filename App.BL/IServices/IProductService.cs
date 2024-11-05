using App.Domain.Entities;
using App.Domain.Models;

namespace App.BL.IServices
{
    public interface IProductService
    {
        public Task<ApiResponse<PaginationResult<ProductDto>>> GetAll(PaginationFilter paginationFilter);
    }
}
