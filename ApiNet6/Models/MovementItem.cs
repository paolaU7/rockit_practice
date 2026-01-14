using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiNet6.Models;

[Table("movement_items")]
public class MovementItem
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("movement_id")]
    [Required]
    public int MovementId { get; set; }

    [ForeignKey("MovementId")]
    public Movement Movement { get; set; } = null!;
    
    [Column("product_id")]
    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    public Product Product { get; set; } = null!;

    [Column("quantity")]
    public int Quantity { get; set; }
    
    [Column("price")]
    public decimal Price { get; set; }
}