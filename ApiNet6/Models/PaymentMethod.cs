using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiNet6.Models;

[Table("payment_method")]
public class PaymentMethod
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("movement_id")]
    public int MovementId { get; set; }

    [ForeignKey("MovementId")]
    public Movement Movement { get; set; } = null!;
    
    [Column("payment_type_id")]
    public int PaymentTypeId { get; set; }

    [ForeignKey("PaymentTypeId")]
    public PaymentTypeEntity PaymentType { get; set; } = null!;
    
    [Column("amount")]
    public decimal Amount { get; set; }
}