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
    public int MovementId { get; set; }
    
    [Column("product_id")]
    public int ProductId { get; set; }
    
    [Column("quantity")]
    public int Quantity { get; set; }
    
    [Column("price")]
    public decimal Price { get; set; }
}