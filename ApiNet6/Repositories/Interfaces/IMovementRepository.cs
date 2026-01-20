using ApiNet6.Models;
using ApiNet6.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiNet6.Repositories;

public interface IMovementRepository : IRepository<Movement>
{
    Task<int> GetDistinctTicketCountAsync();

}