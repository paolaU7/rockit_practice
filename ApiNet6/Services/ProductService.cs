using ApiNet6.Models;
using ApiNet6.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiNet6.Services;

public class ProductService
{
    private readonly IProductRepository _productRepository;
    
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
        {
            throw new Exception("El nombre es requerido");
        }
        if (product.Price <= 0)
        {
            throw new Exception("El precio debe ser mayor a 0");
        }
        return await _productRepository.AddAsync(product);
    }

    public async Task UpdateProductAsync(int id, Product product) 
    {
        if (!await _productRepository.ExistsAsync(id))
        {
            throw new Exception("El producto no existe");
        }
        
        if (string.IsNullOrWhiteSpace(product.Name))
        {
            throw new Exception("El nombre es requerido");
        }
        if (product.Price <= 0)
        {
            throw new Exception("El precio debe ser mayor a 0");
        }
        
        product.Id = id;
        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        if (!await _productRepository.ExistsAsync(id)) 
        {
            throw new Exception("El producto no existe");
        }
        
        try
        {
            await _productRepository.DeleteAsync(id);
        }
        catch (DbUpdateException)
        {
            throw new Exception("No se puede eliminar el producto porque está siendo utilizado en movimientos");
        }
    }
}