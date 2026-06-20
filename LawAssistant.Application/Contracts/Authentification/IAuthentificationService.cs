using LawAssistant.Application.Models;

namespace LawAssistant.Application.Contracts
{
    /// <summary>
    /// Сервис для аутентификации пользователей
    /// </summary>
    public interface IAuthentificationService
    {
        /// <summary>
        /// Выполняет регистрацию пользователя
        /// </summary>
        /// <param name="registerRequest">Модель с данными регистрации</param>
        /// <returns>Модель с данными пользователя</returns>
        public Task<LawyerDto> RegisterAsync(RegisterRequest registerRequest);

        /// <summary>
        /// Выполняет аутентификацию пользователя по логину и паролю
        /// </summary>
        /// <param name="loginRequest">Модель с данными для входа</param>
        /// <returns>Модель с данными пользователя</returns>
        public Task<LawyerDto> LoginAsync(LoginRequest loginRequest);
        
        /// <summary>
        /// Выход из системы
        /// </summary>
        public void Logout();
    }
}
