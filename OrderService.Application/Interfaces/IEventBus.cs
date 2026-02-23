
namespace OrderService.Application.Interfaces;

public interface IEventBus
{
    Task PublishAsync<T>(T message);
}
