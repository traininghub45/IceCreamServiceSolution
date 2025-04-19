

namespace IceCreamService.Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? CreatedBy { get; set; }
    }
}
