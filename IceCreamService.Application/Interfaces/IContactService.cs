// Application/Interfaces/IContactService.cs
using IceCreamService.Core.Entities;

namespace IceCreamService.Application.Interfaces
{
    public interface IContactService
    {
        Task SubmitContactFormAsync(ContactMessage contactMessageDto);
        Task<IEnumerable<ContactMessage>> GetAllMessagesAsync();
        Task<IEnumerable<ContactMessage>> GetUnreadMessagesAsync();
        Task MarkMessageAsReadAsync(int id);
        Task<ContactMessage?> GetByIdAsync(int id);
        Task UpdateAsync(ContactMessage booking);
        Task DeleteByIdAsync(int id);
    }
}