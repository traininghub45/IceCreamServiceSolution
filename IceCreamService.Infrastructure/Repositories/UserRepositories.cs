using IceCreamService.Core.Entities;
using IceCreamService.Core.Interfaces;
using IceCreamService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace IceCreamService.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName == username) ?? null;
        }
    }
}
