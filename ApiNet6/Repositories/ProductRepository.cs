using ApiNet6.Models;
using ApiNet6.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiNet6.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository

{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context) : base(context)  
    {
        _context = context;
    }

    public async Task<List<Product>> GetByIdsAsync(List<int> productIds)
    {
        return await _context.Products
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync();
    }

    public async Task<bool> ProductExistsAsync(int id)
    {
        return await _context.Products.AnyAsync(p => p.Id == id);
    }
}

