using IceCreamService.Core.Entities;

namespace IceCreamService.Core.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<IReadOnlyList<Booking>> GetAllByUserIdAsync(int userId, int pageNumber, int pageSize);

    }
}
