using IceCreamService.Core.Entities;
using IceCreamService.Core.Interfaces;
using IceCreamService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace IceCreamService.Infrastructure.Repositories
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<(IReadOnlyList<Booking> Items, int TotalCount)> GetAllByUserIdAsync(
            int userId,
            int skip = 0,
            int take = 10)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid user ID.", nameof(userId));
            var query = _context.Bookings
                .Where(x => x.UserId == userId);

            var totalRecords = await query.CountAsync();

            var records = await query
                .OrderBy(x => x.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return (records, totalRecords);
        }

    }
}
