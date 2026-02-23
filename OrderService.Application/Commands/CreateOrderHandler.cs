using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Domain.Events;
using Microsoft.Extensions.Logging;
//we have to install the package

namespace OrderService.Application.Commands;

public class CreateOrderHandler
{
    private readonly IOrderRepository _repo;
    private readonly ICacheService _cache;
    private readonly IEventBus _bus;
    private readonly ILogger<CreateOrderHandler> _logger;
    public CreateOrderHandler(IOrderRepository repo, ICacheService cache, IEventBus bus, ILogger<CreateOrderHandler> logger )
    {
        _repo = repo;
        _cache = cache;
        _bus = bus;
        _logger = logger;
    }

    public async Task<Guid> HandleAsync(CreateOrderCommand command)
    {
        var order = new Order(command.Total);
        
        await _repo.AddAsync(order);
        await _cache.SetAsync($"order:{order.Id}", order);
        await _bus.PublishAsync(new OrderCreatedEvent(order.Id));

        _logger.LogInformation("Order {OrderId} created with toltal{Total}", order.Id, order.Total);

        return order.Id;
    }
}
