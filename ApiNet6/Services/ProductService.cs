using ApiNet6.Models;
using ApiNet6.Repositories;

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
        return await _productRepository.GetProductByIdAsync(id);
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllProductsAsync();
    }

    public async Task<Product> CreateProductAsync (Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
        {
            throw new Exception("El nombre es requerido");
        }
        else if (product.Price <= 0)
        {
            throw new Exception("El precio debe ser mayor a 0");
        }
        return await _productRepository.CreateProductAsync(product);
    }

    public async Task UpdateProductAsync(int id, Product product) 
        {
            if (!await _productRepository.ProductExistsAsync(id)) {
                throw new Exception("El producto no existe");
            }
            product.Id  = id;
            await _productRepository.UpdateProductAsync(product);
        }
    
    public async Task DeleteProductAsync(int id)
    {
        if (!await _productRepository.ProductExistsAsync(id)) 
        {
            throw new Exception("El producto no existe");
        }
        await _productRepository.DeleteProductAsync(id);
    }

    public async Task<bool> ProductExistsAsync(int id)
    {
        return await _productRepository.ProductExistsAsync(id);
    }
}

/*
- Un método para obtener UN producto por ID (devuelve Product?) - GetProductByIdAsync
- Un método para obtener TODOS los productos (devuelve List<Product>) - GetAllProductsAsync
- Un método para agregar un producto (recibe Product, devuelve Product) - CreateProductAsync
- Un método para actualizar un producto (recibe Product, no devuelve nada - Task) - UpdateProductAsync
- Un método para eliminar un producto (recibe int id, no devuelve nada - Task) - DeleteProductAsync
- Un método para verificar si existe un producto (recibe int id, devuelve bool) - ProductExistsAsync
*/