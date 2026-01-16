using ApiNet6.Models;

namespace ApiNet6.Repositories;

public interface IProductRepository : IRepository<Product>  // ← Hereda
{
    Task<List<Product>> GetByIdsAsync(List<int> productIds);
}