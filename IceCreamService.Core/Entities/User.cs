
namespace IceCreamService.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required bool IsActive { get; set; }
        public required DateTime CreationDate { get; set; }
        public required string CreatedBy { get; set; }
        public string? phoneNumber { get; set; }
        public string? UserImgProfile { get; set; }
    }
}
