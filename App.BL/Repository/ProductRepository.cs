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
    internal class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse<PaginationResult<ProductDto>>> GetAll(PaginationFilter paginationFilter)
        {
            try
            {
                var totalRecords = await _context.Products
                 .AsNoTracking()
                 .Where(x => x.Name.ToLower().Trim().Contains(paginationFilter.SearchValue.ToLower().Trim()))
                 .CountAsync();

                // Default sorting column to "Product Name" if not provided
                paginationFilter.SortColumn = string.IsNullOrEmpty(paginationFilter.SortColumn) ? "Name" : paginationFilter.SortColumn;
                paginationFilter.SortDirection = string.IsNullOrEmpty(paginationFilter.SortDirection) ? "desc" : paginationFilter.SortDirection;
                paginationFilter.Length = paginationFilter.Length == 0 ? totalRecords : paginationFilter.Length;

                var products = _context.Products
                 .Where(x => x.Name.ToLower().Trim().Contains(paginationFilter.SearchValue.ToLower().Trim())).AsNoTracking()
            .OrderBy($"{paginationFilter.SortColumn} {paginationFilter.SortDirection}")
            .Skip(paginationFilter.Start)
            .Take(paginationFilter.Length);
            var productsDto=_mapper.Map<IEnumerable<ProductDto>>(products);
                var paginationResult = new PaginationResult<ProductDto>();
                paginationResult.TotalRecords = totalRecords;
                paginationResult.Data = productsDto;

                return new ApiResponse<PaginationResult<ProductDto>>(true, new List<string>(),paginationResult);
            }
            catch (Exception ex)
            {
                return new ApiResponse<PaginationResult<ProductDto>>(false, new List<string> { ex.Message }, null);
            }
        }
    }
}
