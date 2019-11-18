using System;

namespace Store.Presentation.Helpers
{
    public class AuthTokenProviderOptions
    {
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public int JwtExpireMinutes { get; set; }
        public TimeSpan AccessTokenExpiration { get; set; } = TimeSpan.FromMinutes(10);
        public TimeSpan RefreshTokenExpiration { get; set; } = TimeSpan.FromDays(60);
    }
}
