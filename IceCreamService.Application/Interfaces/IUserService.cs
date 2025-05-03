using IceCreamService.Core.Entities;
using Microsoft.AspNetCore.Http;


namespace IceCreamService.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User userDto);
        Task<User> UpdateAsync(User userDto, IFormFile file);
        Task DeleteAsync(int id);
        Task ResetPasswordAsync(string token, string newPassword);
    }
}
