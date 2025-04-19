using IceCreamService.Core.Entities;

namespace IceCreamService.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
