using App.DAL.Models;
using App.Domain.Entities;

namespace App.DAL
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetAll(PaginationFilter paginationFilter);
        public Task<int> GetTotalRecords(string searchValue);
        public Task<Product> Get(int productId);
    }
}
