using IceCreamService.Core.Entities;

namespace IceCreamService.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
    }
}
