namespace LawAssistant.Application.Contracts
{
    /// <summary>
    /// Сервис для хеширования строк
    /// </summary>
    public interface IHashService
    {
        /// <summary>
        /// Хеширует строку
        /// </summary>
        /// <param name="str">Строка для хеширования</param>
        /// <returns>Захешированная строка</returns>
        public string Hash(string str);

        /// <summary>
        /// Проверяет строку на соответствие хешу
        /// </summary>
        /// <param name="str">Строка</param>
        /// <param name="hashedStr">Хеш строки</param>
        /// <returns>Признак соответствия строки и захешированной версии</returns>
        public bool Verify(string str, string hashedStr);
    }
}
