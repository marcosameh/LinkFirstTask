using App.BL.Models;
using App.DAL.Models;

namespace App.BL.IServices
{
    public interface IProductService
    {
        public Task<ApiResponse<PaginationResult<ProductDto>>> GetAll(PaginationFilter paginationFilter);
    }
}
