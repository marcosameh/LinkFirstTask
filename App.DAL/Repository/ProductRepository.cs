using App.DAL.Models;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace App.DAL.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;
        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Product> Get(int productId)
        {
            try
            {
                return await _context.Products.FindAsync(productId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<IEnumerable<Product>> GetAll(PaginationFilter paginationFilter)
        {
            try
            {
                var query = _context.Products.AsNoTracking();

                if (!string.IsNullOrEmpty(paginationFilter.SearchValue))
                {
                    var searchValue = paginationFilter.SearchValue.ToLower().Trim();
                    query = query.Where(x => x.Name.ToLower().Contains(searchValue));
                }

                var paginatedProducts = await query
                    .OrderBy($"{paginationFilter.SortColumn} {paginationFilter.SortDirection}")
                    .Skip(paginationFilter.Start)
                    .Take(paginationFilter.Length)
                    .ToListAsync();

                return paginatedProducts;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> GetTotalRecords(string searchValue)
        {
            try
            {
                var query = _context.Products.AsNoTracking();

                if (!string.IsNullOrEmpty(searchValue))
                {
                    searchValue = searchValue.ToLower().Trim();
                    query = query.Where(x => x.Name.ToLower().Contains(searchValue));
                }
                return await query.CountAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
