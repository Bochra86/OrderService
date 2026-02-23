using Confluent.Kafka;

namespace OrderService.Worker.Consumers;
public class OrderCreatedConsumer : BackgroundService
{
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly string _topic;
    private readonly ILogger<OrderCreatedConsumer> _logger;

    public OrderCreatedConsumer(IConsumer<Ignore, string> consumer, IConfiguration config, ILogger<OrderCreatedConsumer> logger )
    {
        _consumer = consumer;
        _topic = config["Kafka:Topic"]!;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        _consumer.Subscribe(_topic);

        while (!ct.IsCancellationRequested)
        {
            var msg = _consumer.Consume(ct);
            Console.WriteLine($"Order event received: {msg.Message.Value}");
            _logger.LogInformation ("Kafka event received: {Event}", msg.Message.Value);
        }

        _consumer.Close();
    }
}