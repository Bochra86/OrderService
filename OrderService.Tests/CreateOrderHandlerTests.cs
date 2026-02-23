using Microsoft.Extensions.Logging;
using Moq;
using OrderService.Application.Commands;
using OrderService.Application.Interfaces;
using OrderService.Domain;               
using OrderService.Domain.Entities;
using OrderService.Domain.Events;
using System.Threading.Tasks;
using Xunit;

namespace OrderService.Tests;

public class CreateOrderHandlerTests
{
    private readonly Mock<IOrderRepository> _repoMock;
    private readonly Mock<ICacheService> _cacheMock;
    private readonly Mock<IEventBus> _eventBusMock;
    private readonly Mock<ILogger<CreateOrderHandler>> _loggerMock;

    private readonly CreateOrderHandler _handler;

    public CreateOrderHandlerTests()
    {
        _repoMock = new Mock<IOrderRepository>();
        _cacheMock = new Mock<ICacheService>();
        _eventBusMock = new Mock<IEventBus>();
        _loggerMock = new Mock<ILogger<CreateOrderHandler>>();

        _handler = new CreateOrderHandler(
            _repoMock.Object,
            _cacheMock.Object,
            _eventBusMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnId()
    {
        // 1. Arrange
        var cmd = new CreateOrderCommand(50);

        _repoMock
            .Setup(x => x.AddAsync(It.IsAny<Order>()))
            .Returns(Task.CompletedTask);

        _eventBusMock
            .Setup(x => x.PublishAsync(It.IsAny<OrderCreatedEvent>()))
            .Returns(Task.CompletedTask);

        // 2. Act
        var id = await _handler.HandleAsync(cmd);

        // 3. Assert
        Assert.NotEqual(Guid.Empty, id);

        _repoMock.Verify(x => x.AddAsync(It.IsAny<Order>()), Times.Once);
        _eventBusMock.Verify(x => x.PublishAsync(It.IsAny<OrderCreatedEvent>()), Times.Once);
    }
}