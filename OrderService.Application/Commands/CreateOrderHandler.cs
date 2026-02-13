using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;

namespace OrderService.Application.Commands;

public class CreateOrderHandler
{
    private readonly IOrderRepository _repo;

    public CreateOrderHandler(IOrderRepository repo)
    {
        _repo = repo;
    }

    public async Task<Guid> Handle(CreateOrderCommand command)
    {
        var order = new Order(command.Total);
        await _repo.AddAsync(order);
        return order.Id;
    }
}
