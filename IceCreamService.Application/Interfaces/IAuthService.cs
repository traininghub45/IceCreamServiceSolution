using IceCreamService.Application.DTOs;
using IceCreamService.Core.Entities;

namespace IceCreamService.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> AuthenticateAsync(string userName , string password);
    }
}
