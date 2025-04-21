using AutoMapper;
using IceCreamService.Application.Interfaces;
using IceCreamService.Core.Entities;
using IceCreamService.Core.Interfaces;

namespace IceCreamService.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        public BookingService(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _bookingRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
           return await _bookingRepository.GetAllAsync();
        }

        public async Task AddAsync(Booking booking)
        {
            await _bookingRepository.AddAsync(booking);
        }

        public async Task UpdateAsync(Booking booking)
        {
            await _bookingRepository.UpdateAsync(booking);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _bookingRepository.DeleteByIdAsync(id);
        }
    }
}
