using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Persistence;
internal class OrderRepository: IOrderRepository
{
    public AppDbContext _dbContext;
    public OrderRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task  AddAsync(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }
}

