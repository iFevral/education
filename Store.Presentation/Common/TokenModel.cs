namespace Store.Presentation.Common
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public int ATExpires { get; set; } = 0;
        public string RefreshToken { get; set; }
        public int RTExpires { get; set; } = 0;
        public string Username { get; set; }
    }
}
