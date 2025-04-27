using IceCreamService.Infrastructure.Configurations;
using IceCreamService.Application.Configurations;
using IceCreamService.API.Configurations;
using IceCreamService.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Register services from all layers
builder.Services
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices()
    .AddApiServices(builder.Configuration);

var app = builder.Build();

// Configure middleware pipeline
app.UseApiMiddleware();

app.Run();