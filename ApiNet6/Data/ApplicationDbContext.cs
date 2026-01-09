using Microsoft.EntityFrameworkCore;
using ApiNet6.Models;

namespace ApiNet6.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Movement> Movements { get; set; } = null!;
    public DbSet<MovementItem> MovementItems { get; set; } = null!;
    public DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
    public DbSet<PaymentTypeEntity> PaymentTypes { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
}