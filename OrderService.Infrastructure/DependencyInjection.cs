using Confluent.Kafka;
using StackExchange.Redis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Interfaces;
using OrderService.Infrastructure.Messaging;
using OrderService.Infrastructure.Caching;
using OrderService.Infrastructure.Persistence;


namespace OrderService.Infrastructure;

public static class DependencyInjection
{
    //extension method for IServiceCollection to add infrastructure services
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration config)
    {
        // DB
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(config.GetConnectionString("OrderServiceDb")));
        services.AddScoped<IOrderRepository, OrderRepository>();

   
        // Redis
        var redisConnection = config.GetConnectionString("Redis") ?? throw new InvalidOperationException("Redis connection string is missing");

        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisConnection));
        services.AddScoped<ICacheService, RedisCacheService>();


        // Kafka Producer 
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = config["Kafka:BootstrapServers"]
        };

        services.AddSingleton<IProducer<Null, string>>( _ => new ProducerBuilder<Null, string>(producerConfig).Build());

        services.AddSingleton<IEventBus, KafkaProducer>();

        // Kafka Consumer (just the client)
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = config["Kafka:BootstrapServers"],
            GroupId = config["Kafka:GroupId"],
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        services.AddSingleton<IConsumer<Ignore, string>>( _ => new ConsumerBuilder<Ignore, string>(consumerConfig).Build());

        return services;
    }
}