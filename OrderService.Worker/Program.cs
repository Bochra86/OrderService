using OrderService.Infrastructure;
using OrderService.Worker.Consumers;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build())
    .Enrich.FromLogContext()
    .CreateLogger();

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSerilog();


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHostedService<OrderCreatedConsumer>();

var host = builder.Build();
await host.RunAsync();