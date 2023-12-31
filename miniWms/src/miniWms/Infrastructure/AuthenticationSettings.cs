namespace miniWms.Infrastructure
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; } = null!;
        public int JwtExpireDaysForNormalLogin { get; set; }
        public string JwtIssuer { get; set; } = null!;
    }
}
