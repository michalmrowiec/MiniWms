namespace miniWms.Domain.Models
{
    public class JwtToken
    {
        public string UserEmail { get; set; }
        public string Token { get; set; }
        public bool HaveToChangePassword { get; set; } = false;
    }
}
