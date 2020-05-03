namespace Blog.Services.Authentication
{
    public class JwtTokenOptions
    {
        public string SigningSecret { get; set; } = null!;
        
        public string Issuer { get; set; } = null!;
        
        public int ExpiresInMinutes { get; set; }
        
        
        public int RefreshExpiresInMinutes { get; set; }
    }
}