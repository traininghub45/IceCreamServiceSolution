using IceCreamService.Core.Entities;

namespace IceCreamService.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User booking);
        Task UpdateAsync(User booking);
        Task DeleteAsync(int id);
        Task<User> GetByUsernameAsync(string username);
        Task<bool> UserExistsAsync(string username);
    }
}
