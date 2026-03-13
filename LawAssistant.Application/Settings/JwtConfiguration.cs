namespace LawAssistant.Application.Settings
{
    public class JwtConfiguration
    {
        public int ExpirationTimeInMinutes { get; set; }
        public string SecretKey { get; set; }
    }
}
