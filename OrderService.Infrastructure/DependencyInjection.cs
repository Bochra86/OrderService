using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using OrderService.Application.Interfaces;
using OrderService.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("OrderServiceDb")));

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICacheService, RedisCacheService>();

        return services;
    }
}
