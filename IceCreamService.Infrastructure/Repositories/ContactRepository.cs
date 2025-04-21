using IceCreamService.Core.Entities;
using IceCreamService.Core.Interfaces;
using IceCreamService.Infrastructure.Data;

namespace IceCreamService.Infrastructure.Repositories
{
    public class ContactRepository : Repository<ContactMessage>, IContactRepository
    {
        public ContactRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task MarkAsReadAsync(int id)
        {
            var message = await GetByIdAsync(id);
            if (message != null)
            {
                message.IsRead = true;
                await UpdateAsync(message);
            }
        }

        public async Task<IEnumerable<ContactMessage>> GetUnreadMessagesAsync()
        {
            return await FindAsync(m => !m.IsRead);
        }
    }
}