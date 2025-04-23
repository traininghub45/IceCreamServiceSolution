namespace IceCreamService.Core.Entities
{
    public class AuthResult
    {
        public string? Token { get; set; }
        public User? User { get; set; }
    }
}
