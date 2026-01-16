using ApiNet6.Models;
using ApiNet6.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiNet6.Repositories;

public class MovementRepository : Repository<Movement>, IMovementRepository
{
    private readonly ApplicationDbContext _context;

    public MovementRepository(ApplicationDbContext context) : base(context)
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

    Task IMovementRepository.AddAsync(Movement movement)
    {
        return AddAsync(movement);
    }
}