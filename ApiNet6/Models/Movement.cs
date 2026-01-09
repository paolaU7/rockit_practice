using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiNet6.Models;

[Table("movement")]
public class Movement
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("ticket_number")]
    [MaxLength(50)]
    public string TicketNumber { get; set; } = "";
    
    [Column("date")]
    public DateTime Date { get; set; }
    
    [Column("time")]
    public TimeSpan Time { get; set; }
    
    [Column("cuit")]
    public long Cuit { get; set; }
    
    [Column("total")]
    public decimal Total { get; set; }
}