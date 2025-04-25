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