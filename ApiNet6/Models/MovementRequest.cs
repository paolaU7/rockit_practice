namespace ApiNet6.Models;

public class MovementRequest
{
    public string Name { get; set; } = "";  
    public long Cuit { get; set; }  
    public List<ProductItem> Products { get; set; } = new();
    public List<PaymentItem> Payments { get; set; } = new(); 
    public decimal Total { get; set; } 
}

public class PaymentItem
{
    public PaymentType Type { get; set; }
    public decimal Amount { get; set; }
}