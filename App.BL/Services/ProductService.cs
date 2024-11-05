using App.BL.IServices;
using App.Domain.Entities;
using App.Domain.Models;
using App.Infrastructure;
using AutoMapper;

namespace App.BL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductService(IProductRepository productRepository,
            IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }


        public async Task<ApiResponse<PaginationResult<ProductDto>>> GetAll(PaginationFilter paginationFilter)
        {
            try
            {
                // Set default sorting column and direction if not provided
                paginationFilter.SortColumn = string.IsNullOrEmpty(paginationFilter.SortColumn) ? "Price" : paginationFilter.SortColumn;
                paginationFilter.SortDirection = string.IsNullOrEmpty(paginationFilter.SortDirection) ? "asc" : paginationFilter.SortDirection;
                var paginatedProducts = await productRepository.GetAll(paginationFilter);
                var totalRecords = await productRepository.GetTotalRecords(paginationFilter.SearchValue);

                var productsDto = mapper.Map<IEnumerable<ProductDto>>(paginatedProducts);

                var paginationResult = new PaginationResult<ProductDto>
                {
                    TotalRecords = totalRecords,
                    Data = productsDto
                };

                return new ApiResponse<PaginationResult<ProductDto>>(true, new List<string>(), paginationResult);
            }
            catch (Exception ex)
            {
                return new ApiResponse<PaginationResult<ProductDto>>(false, new List<string> { ex.Message}, null);
            }
        }
    }
}
