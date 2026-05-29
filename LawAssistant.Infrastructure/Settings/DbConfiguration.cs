namespace LawAssistant.Infrastructure.Settings
{
    /// <summary>
    /// Настройки подключения к БД
    /// </summary>
    public class DbConfiguration
    {
        /// <summary>
        /// Строка подключения к БД PostgreSQL
        /// </summary>
        public string PostreSqlConnectionString { get; set; }
    }
}
