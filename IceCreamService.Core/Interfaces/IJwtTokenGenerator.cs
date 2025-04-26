using IceCreamService.Core.Entities;

namespace IceCreamService.Core.Interfaces
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateTokenAsync(User user);
    }
}
