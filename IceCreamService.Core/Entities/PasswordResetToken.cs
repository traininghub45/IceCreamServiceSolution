
namespace IceCreamService.Core.Entities
{
    public class PasswordResetToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
