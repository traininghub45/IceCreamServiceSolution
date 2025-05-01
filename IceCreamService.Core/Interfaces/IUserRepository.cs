using IceCreamService.Core.Entities;

namespace IceCreamService.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task SavePasswordResetTokenAsync(int userId, string token, DateTime expirationTime);
        Task ResetPasswordAsync(string token, string newPassword);
    }
}
