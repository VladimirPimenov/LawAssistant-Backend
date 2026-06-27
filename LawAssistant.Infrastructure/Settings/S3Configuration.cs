namespace LawAssistant.Infrastructure.Settings
{
    /// <summary>
    /// Настройки S3 хранилища
    /// </summary>
    public class S3Configuration
    {
        /// <summary>
        /// Адрес S3 хранилища
        /// </summary>
        public string Url { get; set; }
        
        public string Login { get; set; }
        
        public string Password { get; set; }
        
        public string DocumentsBucketName { get; set; }
        
        public bool UseSsl { get; set; }
    }
}
