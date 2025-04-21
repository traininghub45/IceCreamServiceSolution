using IceCreamService.Core.Entities;

namespace IceCreamService.Core.Interfaces
{
    public interface IContactRepository : IRepository<ContactMessage>
    {
        Task MarkAsReadAsync(int id);
        Task<IEnumerable<ContactMessage>> GetUnreadMessagesAsync();
    }
}