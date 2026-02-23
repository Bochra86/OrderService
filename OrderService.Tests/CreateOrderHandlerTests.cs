namespace OrderService.Tests;

public class CreateOrderHandlerTests
{
  
    private readonly Moq<IOrderRepository> _moqRepo;
    private readonly Moq<ICacheService> _moqRedis;
    private readonly Moq<IEventBus> _moqKafka;
    private readonly Moq<ILogger<CreateOrderHandler>> _moqLogging;

    private readonly CreateOrderHandler _handler;

    public CreateOrderHandlerTests( )
    {
        _moqRepo = new Moq<IOrderRepository>(); 
        _moqRedis = new Moq<ICacheService>();   
        _moqKafka = new Moq<IEventBus>();   
        _moqLogging = new Moq<ILogger<CreateOrderHandler>>;
        
        _handler = new CreateOrderHandler(_moqRepo.Object, _moqRedis.Object, _moqKafka.Object, _moqLogging.Object)
    }


    [Fact]
    public void HandleAsync_ShouldRetursId()
    {
        //1.Arrange
        var cmd = new CreateOrderCommand(50);
        var order= new Order(cmd.Total);

        _moqRepo.Setup(x=> x.AddAsync(It.IsAny<object>()))
            .ReturnsAsync(null);

        _moqKafka.Setup(x=> x.PublishAsync(It.IsAny<object>()))
            .ReturnsAsync(Task.CompletedTask);

        //2. Act
        var id = await _handler.HandleAsync(cmd);

        //3.Assert
        Assert.NotNull(id);
        _moqRepo.Verify(x => x.AddAsync(order), Times.Once);
        _moqKafka.Verify(x => x.PublishAsync(new OrderCreatedEvent(order.Id)), Times.Once);    
    }
}
