using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiNet6.Models;

[Table("products")]
public class Product
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("name")]
    [MaxLength(100)]
    public string Name { get; set; } = "";
    
    [Column("price")]
    public decimal Price { get; set; }
}