namespace IceCreamService.Core.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime EventDate { get; set; }
        public string? Location { get; set; }
        public int NumberOfGuests { get; set; }
        public string? IceCreamPreferences { get; set; }
        public bool IsApproved { get; set; }
    }
}
