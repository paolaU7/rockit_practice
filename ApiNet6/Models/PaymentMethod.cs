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
    
    [Column("payment_type_id")]
    public int PaymentTypeId { get; set; }
    
    [Column("amount")]
    public decimal Amount { get; set; }
}