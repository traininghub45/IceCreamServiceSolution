using IceCreamService.Application.Interfaces;
using IceCreamService.Core.Entities;
using IceCreamService.Core.Interfaces;

namespace IceCreamService.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task SubmitContactFormAsync(ContactMessage contactMessage)
        {
            await _contactRepository.AddAsync(contactMessage);
        }

        public async Task<IEnumerable<ContactMessage>> GetAllMessagesAsync()
        {
            return await _contactRepository.GetAllAsync();
        }

        public async Task<IEnumerable<ContactMessage>> GetUnreadMessagesAsync()
        {
            return await _contactRepository.GetUnreadMessagesAsync();
        }

        public async Task MarkMessageAsReadAsync(int id)
        {
            await _contactRepository.MarkAsReadAsync(id);
        }

        public async Task<ContactMessage?> GetByIdAsync(int id)
        {
            return await _contactRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(ContactMessage contactMessage)
        {
            await _contactRepository.UpdateAsync(contactMessage);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _contactRepository.DeleteByIdAsync(id);
        }
    }
}