using ApiNet6.Models;
using ApiNet6.Data;
using ApiNet6.Repositories; 
using ApiNet6.Rules;
using ApiNet6.Rules.Movement;
using Microsoft.EntityFrameworkCore;

namespace ApiNet6.Services;

public class HarmonizedService
{
    private readonly RockitService _rockit;
    private readonly IProductRepository _productRepository;
    private readonly IMovementRepository _movementRepository;
    private readonly IRepository<MovementItem> _movementItemRepository;
    private readonly IRepository<PaymentMethod> _paymentMethodRepository;
    private readonly RuleEngine<MovementRequest> _ruleEngine;

    public HarmonizedService(
        RockitService rockit,
        IProductRepository productRepository,
        IMovementRepository movementRepository,
        IRepository<MovementItem> movementItemRepository,
        IRepository<PaymentMethod> paymentMethodRepository)
    {
        _rockit = rockit;
        _productRepository = productRepository;
        _movementRepository = movementRepository;
        _movementItemRepository = movementItemRepository;
        _paymentMethodRepository = paymentMethodRepository;
        
        _ruleEngine = new RuleEngine<MovementRequest>();
        _ruleEngine.AddRule(new CuitValidRule());
        _ruleEngine.AddRule(new NameRequiredRule());
    }

    public async Task<object> SendStringAsync(string rawData)
    {
        var movementRequest = _rockit.StringToObject(rawData);
        
        var validationResult = await _ruleEngine.ValidateAsync(movementRequest);
        
        if (!validationResult.IsValid)
        {
            throw new Exception(string.Join(", ", validationResult.Errors));
        }
        
        var productIds = movementRequest.Products.Select(p => p.ProductId).ToList();
        var existingProducts = await _productRepository.GetByIdsAsync(productIds);
        
        var missingProductIds = productIds.Except(existingProducts.Select(p => p.Id)).ToList();
        if (missingProductIds.Any())
        {
            throw new Exception($"Los siguientes productos no existen en la base de datos: {string.Join(", ", missingProductIds)}");
        }
        
        var movementCount = await _movementRepository.GetDistinctTicketCountAsync();
        
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
        
        await _movementRepository.AddAsync(movement);
        
        var movementItems = movementRequest.Products.Select(productItem => 
        {
            var product = existingProducts.First(p => p.Id == productItem.ProductId);
            return new MovementItem
            {
                MovementId = movement.Id,
                ProductId = product.Id,
                Quantity = productItem.Quantity,
                Price = product.Price
            };
        }).ToList();
        
        foreach (var item in movementItems)
        {
            await _movementItemRepository.AddAsync(item);
        }
        
        var paymentMethods = movementRequest.Payments.Select(paymentItem => 
            new PaymentMethod
            {
                MovementId = movement.Id,
                PaymentTypeId = (int)paymentItem.Type + 1,
                Amount = paymentItem.Amount
            }).ToList();
        
        foreach (var payment in paymentMethods)
        {
            await _paymentMethodRepository.AddAsync(payment);
        }
        
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