using ApiNet6.Models;
using ApiNet6.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiNet6.Repositories;

public class MovementItemRepository : IMovementItemRepository
{
    private readonly ApplicationDbContext _context;

    public MovementItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(MovementItem item)
    {
        _context.MovementItems.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<MovementItem> items)
    {
        _context.MovementItems.AddRange(items);
        await _context.SaveChangesAsync();
    }
}