using ApiNet6.Models;
using ApiNet6.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiNet6.Services;

public class HarmonizedService
{
    private readonly RockitService _rockit;
    private readonly ApplicationDbContext _context;

    public HarmonizedService(RockitService rockit, ApplicationDbContext context)
    {
        _rockit = rockit;
        _context = context;
    }

    public async Task<object> SendStringAsync(string rawData)
    {
        var movementRequest = _rockit.StringToObject(rawData);
        
        var productIds = movementRequest.Products.Select(p => p.ProductId).ToList();
        var existingProducts = await _context.Products
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync();
        
        var missingProductIds = productIds.Except(existingProducts.Select(p => p.Id)).ToList();
        if (missingProductIds.Any())
        {
            throw new Exception($"Los siguientes productos no existen en la base de datos: {string.Join(", ", missingProductIds)}");
        }
        
        var movementCount = await _context.Movements
            .Select(m => m.TicketNumber)
            .Distinct()
            .CountAsync();
        
        string ticketNumber = $"T-{(movementCount + 1):D3}";
        DateTime currentDate = DateTime.Now;
        
        decimal total = 0;
        var productDetails = new List<object>();
        
        foreach (var productItem in movementRequest.Products)
        {
            var product = existingProducts.First(p => p.Id == productItem.ProductId);
            decimal subtotal = product.Price * productItem.Quantity;
            total += subtotal;
            
            productDetails.Add(new
            {
                productId = product.Id,
                name = product.Name,
                price = product.Price,
                quantity = productItem.Quantity,
                subtotal = subtotal
            });
        }
        
        var movement = new Movement
        {
            TicketNumber = ticketNumber,
            Date = currentDate.Date,
            Time = currentDate.TimeOfDay,
            Cuit = movementRequest.Cuit,
            Total = total
        };
        
        _context.Movements.Add(movement);
        await _context.SaveChangesAsync();  
        
        foreach (var productItem in movementRequest.Products)
        {
            var product = existingProducts.First(p => p.Id == productItem.ProductId);
            
            var movementItem = new MovementItem
            {
                MovementId = movement.Id,
                ProductId = product.Id,
                Quantity = productItem.Quantity,
                Price = product.Price  
            };
            
            _context.MovementItems.Add(movementItem);
        }
        
        foreach (var paymentItem in movementRequest.Payments)
        {
            var paymentMethod = new PaymentMethod
            {
                MovementId = movement.Id,
                PaymentTypeId = (int)paymentItem.Type + 1,  
                Amount = paymentItem.Amount
            };
            
            _context.PaymentMethods.Add(paymentMethod);
        }
        
        await _context.SaveChangesAsync();
        
        return new
        {
            ticketNumber = ticketNumber,
            date = currentDate.ToString("yyyy-MM-dd"),
            time = currentDate.ToString("HH:mm:ss"),
            name = movementRequest.Name,
            cuit = movementRequest.Cuit,
            products = productDetails,
            payments = movementRequest.Payments,
            total = total
        };
    }
}