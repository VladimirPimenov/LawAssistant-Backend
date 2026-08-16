using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Contracts
{
    /// <summary>
    /// Класс для генерации токенов аутентификации
    /// </summary>
    public interface ITokenProvider
    {
        /// <summary>
        /// Генерирует токен для аккаунта
        /// </summary>
        /// <param name="account">Аккаунт</param>
        /// <returns>Токен</returns>
        public string GenerateToken(Account account);
    }
}
