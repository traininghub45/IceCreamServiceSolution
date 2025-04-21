using IceCreamService.Core.Entities;
using IceCreamService.Core.Interfaces;
using IceCreamService.Infrastructure.Data;


namespace IceCreamService.Infrastructure.Repositories
{
    public class BookingRepository : Repository<Booking>,IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
