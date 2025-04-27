using IceCreamService.Application.Interfaces;
using IceCreamService.Application.Services;
using IceCreamService.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace IceCreamService.Application.Configurations;
public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // AutoMapper
        services.AddAutoMapper(typeof(MappingProfile));

        // Application Services
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IContactService, ContactService>();

        return services;
    }
}