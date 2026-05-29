namespace LawAssistant.Application.Settings
{
    /// <summary>
    /// Настройки JWT токенов
    /// </summary>
    public class JwtConfiguration
    {
        /// <summary>
        /// Время жизни токена в минутах
        /// </summary>
        public int ExpirationTimeInMinutes { get; set; }
        
        /// <summary>
        /// Секретный ключ для формирования токенов
        /// </summary>
        public string SecretKey { get; set; }
    }
}
