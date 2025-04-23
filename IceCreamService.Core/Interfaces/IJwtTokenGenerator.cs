using IceCreamService.Core.Entities;

namespace IceCreamService.Core.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
