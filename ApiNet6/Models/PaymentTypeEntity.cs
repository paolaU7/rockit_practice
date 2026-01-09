using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiNet6.Models;

[Table("payment_type")]
public class PaymentTypeEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("name")]
    [MaxLength(50)]
    public string Name { get; set; } = "";
}