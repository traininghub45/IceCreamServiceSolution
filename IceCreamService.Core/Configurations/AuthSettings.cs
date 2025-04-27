namespace IceCreamService.Core.Configurations
{
    public class AuthSettings
    {
        public string Secret { get; set; } = null!;
        public int TokenExpirationMinutes { get; set; }
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public int RefreshTokenExpirationDays { get; set; }
    }
}