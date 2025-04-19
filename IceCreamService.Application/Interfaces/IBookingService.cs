using IceCreamService.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IceCreamService.Application.Interfaces
{
    public interface IBookingService
    {
        Task<BookingDto> GetByIdAsync(int id);
        Task<IEnumerable<BookingDto>> GetAllAsync();
        Task AddAsync(BookingDto bookingDto);
        Task UpdateAsync(BookingDto bookingDto);
        Task DeleteAsync(int id);
    }
}
