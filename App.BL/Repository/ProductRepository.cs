using App.BL.Common;
using App.BL.DTOs;
using App.BL.IRepository;
using App.BL.Models;
using App.DAL.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace App.BL.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<PaginationResult<ProductDto>>> GetAll(PaginationFilter paginationFilter)
        {
            try
            {
                var query = _context.Products.AsNoTracking();

                
                if (!string.IsNullOrEmpty(paginationFilter.SearchValue))
                {
                    var searchValue = paginationFilter.SearchValue.ToLower().Trim();
                    query = query.Where(x => x.Name.ToLower().Contains(searchValue));
                }

              
                var totalRecords = await query.CountAsync();

                // Set default sorting column and direction if not provided
                var sortColumn = string.IsNullOrEmpty(paginationFilter.SortColumn) ? "Price" : paginationFilter.SortColumn;
                var sortDirection = string.IsNullOrEmpty(paginationFilter.SortDirection) ? "asc" : paginationFilter.SortDirection;

                
                var paginatedProducts = await query
                    .OrderBy($"{sortColumn} {sortDirection}")
                    .Skip(paginationFilter.Start)
                    .Take(paginationFilter.Length)
                    .ToListAsync();

                var productsDto = _mapper.Map<IEnumerable<ProductDto>>(paginatedProducts);

               
                var paginationResult = new PaginationResult<ProductDto>
                {
                    TotalRecords = totalRecords,
                    Data = productsDto
                };

                return new ApiResponse<PaginationResult<ProductDto>>(true, new List<string>(), paginationResult);
            }
            catch (Exception ex)
            {
                return new ApiResponse<PaginationResult<ProductDto>>(false, new List<string> { ex.Message }, null);
            }
        }
    }
}
