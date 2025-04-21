using IceCreamService.Core.Entities;
using IceCreamService.Core.Interfaces;
using IceCreamService.Infrastructure.Data;


namespace IceCreamService.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
