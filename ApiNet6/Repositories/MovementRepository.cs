
using ApiNet6.Models;
using ApiNet6.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiNet6.Repositories;

public class MovementRepository : IMovementRepository
{
    private readonly ApplicationDbContext _context;

    public MovementRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetDistinctTicketCountAsync()
    {
        return await _context.Movements
            .Select(m => m.TicketNumber)
            .Distinct()
            .CountAsync();
    }

    public async Task<Movement> AddAsync(Movement movement)
    {
        _context.Movements.Add(movement);
        await _context.SaveChangesAsync();
        return movement;
    }
}