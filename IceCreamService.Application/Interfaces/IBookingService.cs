using IceCreamService.Application.DTOs;
using IceCreamService.Core.Entities;


namespace IceCreamService.Application.Interfaces
{
    public interface IBookingService
    {
        Task<Booking?> GetByIdAsync(int id);
        Task<IEnumerable<Booking>> GetAllAsync();
        Task AddAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task DeleteByIdAsync(int id);
        Task<IEnumerable<Booking>> GetAllByUserIdAsync(int userId, int pageNumber, int pageSize);

    }
}
