using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Contracts
{
    /// <summary>
    /// Класс для генерации токенов аутентификации
    /// </summary>
    public interface ITokenProvider
    {
        /// <summary>
        /// Генерирует токен
        /// </summary>
        /// <param name="lawyer">Юрист</param>
        /// <returns>Токен</returns>
        public string GenerateToken(Lawyer lawyer);
    }
}
