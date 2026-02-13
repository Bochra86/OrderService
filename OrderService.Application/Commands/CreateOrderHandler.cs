using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;

namespace OrderService.Application.Commands;

public class CreateOrderHandler
{
    private readonly IOrderRepository _repo;
    private readonly ICacheService _cache;
    public CreateOrderHandler(
        IOrderRepository repo,
        ICacheService cache)
    {
        _repo = repo;
        _cache = cache;
    }

    public async Task<Guid> Handle(CreateOrderCommand command)
    {
        var order = new Order(command.Total);

        await _repo.AddAsync(order);
        await _cache.SetAsync($"order:{order.Id}", order);

        return order.Id;
    }
}
