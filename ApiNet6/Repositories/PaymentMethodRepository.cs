using ApiNet6.Models;
using ApiNet6.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiNet6.Repositories;

public class PaymentMethodRepository : IPaymentMethodRepository
{
    private readonly ApplicationDbContext _context;

    public PaymentMethodRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(PaymentMethod paymentMethod)
    {
        _context.PaymentMethods.Add(paymentMethod);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<PaymentMethod> paymentMethods)
    {
        _context.PaymentMethods.AddRange(paymentMethods);
        await _context.SaveChangesAsync();
    }
}