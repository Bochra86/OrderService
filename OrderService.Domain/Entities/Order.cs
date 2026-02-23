namespace OrderService.Domain.Entities;
public class Order
{
    public Guid Id { get; private set; }
    public decimal Total { get; private set; }
    public DateTime CreatedAt { get; private set; }
   
    private Order() { }
    public Order(decimal total)
    {
        Id = Guid.NewGuid();
        Total = total;
        CreatedAt = DateTime.UtcNow;
    }
}