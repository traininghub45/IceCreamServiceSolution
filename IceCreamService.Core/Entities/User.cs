
namespace IceCreamService.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime CreationDate { get; set; }
        public string? CreatedBy { get; set; }
        public ICollection<Booking>? Bookings { get; set; }  // Navigation property to Bookings
    }
}
