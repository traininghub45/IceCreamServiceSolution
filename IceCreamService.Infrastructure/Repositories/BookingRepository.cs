using IceCreamService.Core.Entities;
using IceCreamService.Core.Interfaces;
using IceCreamService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace IceCreamService.Infrastructure.Repositories
{
    public class BookingRepository : Repository<Booking>,IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Booking>> GetAllByUserIdAsync(
            int userId,
            int pageNumber,
            int pageSize
            )
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid user ID.", nameof(userId));

            if (pageNumber <= 0)
                throw new ArgumentException("Page number should be positive.", nameof(pageNumber));

            if (pageSize <= 0)
                throw new ArgumentException("Page size should be positive.", nameof(pageSize));

            return await _context.Bookings
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.Id) // or some other meaningful order
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(default);
        }
    }
}
