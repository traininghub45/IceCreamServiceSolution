using IceCreamService.Core.Entities;

namespace IceCreamService.Core.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<(IReadOnlyList<Booking> Items, int TotalCount)> GetAllByUserIdAsync(int userId, int skip, int take);

    }
}
