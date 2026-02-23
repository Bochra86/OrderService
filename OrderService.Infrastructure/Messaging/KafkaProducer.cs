using OrderService.Application.Interfaces;

using Confluent.Kafka;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace OrderService.Infrastructure.Messaging;

public class KafkaProducer : IEventBus
{
    private readonly IProducer<Null, string> _producer;
    private readonly string _topic;

    public KafkaProducer(IProducer<Null, string> producer,IConfiguration config)
    {
        _producer = producer;
        _topic = config["Kafka:Topic"]!;
    }

    public async Task PublishAsync<T>(T message)
    {
        await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = JsonSerializer.Serialize(message) });
    }
}