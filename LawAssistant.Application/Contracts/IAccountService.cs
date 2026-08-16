using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Contracts
{
    /// <summary>
    /// Сервис для работы с аккаунтами пользователей
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Получает аккаунт по идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор аккаунта</param>
        /// <returns>Найденный юрист</returns>
        public Task<Account> GetAccountAsync(int accountId);

        /// <summary>
        /// Получает аккаунт по Email
        /// </summary>
        /// <param name="accountId">Email юриста</param>
        /// <returns>Найденный юрист</returns>
        public Task<Account> GetAccountByEmailAsync(string email);

        /// <summary>
        /// Создает аккаунт
        /// </summary>
        /// <param name="lawyer">Модель аккаунта</param>
        /// <returns>Созданный аккаунт</returns>
        public Task<Account> CreateAccountAsync(Account account);

        /// <summary>
        /// Изменяет данные аккаунта
        /// </summary>
        /// <param name="lawyerDto">Модель с обновлёнными данными аккаунта</param>
        /// <returns>Модель юриста с изменёнными полями</returns>
        public Task<AccountDto> UpdateAccountInfoAsync(AccountDto account);

        /// <summary>
        /// Изменяет пароль аккаунта
        /// </summary>
        /// <param name="account">Аккаунт</param>
        /// <returns>Модель аккаунта с изменёнными полями</returns>
        public Task<AccountDto> ChangePasswordAsync(Account account);
    }
}
