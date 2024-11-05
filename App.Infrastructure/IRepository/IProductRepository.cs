using App.Domain.Entities;
using App.Domain.Models;

namespace App.Infrastructure
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetAll(PaginationFilter paginationFilter);
        public Task<int> GetTotalRecords(string searchValue);
        public Task<Product> Get(int productId);
    }
}
