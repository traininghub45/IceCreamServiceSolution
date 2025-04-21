using IceCreamService.Core.Entities;


namespace IceCreamService.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User userDto);
        Task UpdateAsync(User userDto);
        Task DeleteAsync(int id);
    }
}
