using Microsoft.EntityFrameworkCore;
using ApiNet6.Models;

namespace ApiNet6.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }

    public DbSet<Movement> Movements { get; set; } = null!;
    public DbSet<MovementItem> MovementItems { get; set; } = null!;
    public DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
    public DbSet<PaymentTypeEntity> PaymentTypes { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Movement>()
            .HasMany(m => m.MovementItems)
            .WithOne(mi => mi.Movement)
            .HasForeignKey(mi => mi.MovementId)
            .OnDelete(DeleteBehavior.Cascade);  

        modelBuilder.Entity<Movement>()
            .HasMany(m => m.PaymentMethods)
            .WithOne(pm => pm.Movement)
            .HasForeignKey(pm => pm.MovementId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MovementItem>()
            .HasOne<MovementItem>()
            .WithMany()
            .HasForeignKey(mi => mi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PaymentMethod>()
            .HasOne<PaymentMethod>()
            .WithMany()
            .HasForeignKey(pm => pm.PaymentTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}