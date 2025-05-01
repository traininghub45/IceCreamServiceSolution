using AutoMapper;
using IceCreamService.Application.DTOs;
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

        public async Task<PagedResult<Booking>> GetAllByUserIdAsync(int userId, int skip, int take)
        {
            var result = await _bookingRepository.GetAllByUserIdAsync(userId, skip, take);

            return new PagedResult<Booking>
            {
                Data = result.Items,
                TotalCount = result.TotalCount
            };
        }
    }
}
sync(userId, pageNumber, pageSize);
        }
    }
}
