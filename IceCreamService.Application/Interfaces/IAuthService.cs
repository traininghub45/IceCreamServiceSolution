using IceCreamService.Application.DTOs;

namespace IceCreamService.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(string userName , string password);
        Task RegisterAsync(UserDto request);
    }
}
