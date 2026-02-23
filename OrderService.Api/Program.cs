using Ecommerce.Api.Middlewares;
using OrderService.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext();
});
builder.Host.UseSerilog();


var dbConnection = builder.Configuration.GetConnectionString("OrderServiceDb")
    ?? throw new InvalidOperationException("OrderServiceDb connection string is missing.");

var redisConnection = builder.Configuration.GetConnectionString("Redis")
    ?? throw new InvalidOperationException("Redis connection string is missing.");

builder.Services.AddHealthChecks()
    .AddNpgSql(dbConnection)
    .AddRedis(redisConnection);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseCors();

app.MapHealthChecks("/health");
app.MapControllers();

app.Run();