using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Contracts
{
    /// <summary>
    /// Сервис для работы с юристами
    /// </summary>
    public interface ILawyerService
    {
        /// <summary>
        /// Возвращает список юристов
        /// </summary>
        /// <returns>Список юристов</returns>
        public Task<List<LawyerDto>> GetLawyersListAsync();

        /// <summary>
        /// Получает юриста по идентификатору
        /// </summary>
        /// <param name="lawyerId">Идентификатор юриста</param>
        /// <returns>Найденный юрист</returns>
        public Task<Lawyer> GetLawyerAsync(int lawyerId);

        /// <summary>
        /// Получает юриста по Email
        /// </summary>
        /// <param name="lawyerId">Email юриста</param>
        /// <returns>Найденный юрист</returns>
        public Task<Lawyer> GetLawyerByEmailAsync(string email);

        /// <summary>
        /// Создает юриста
        /// </summary>
        /// <param name="lawyer">Модель юриста</param>
        /// <returns>Созданный юрист</returns>
        public Task<Lawyer> CreateLawyerAsync(Lawyer lawyer);

        /// <summary>
        /// Изменяет данные юриста
        /// </summary>
        /// <param name="lawyerDto">Модель с обновлёнными данными юриста</param>
        /// <returns>Модель юриста с изменёнными полями</returns>
        public Task<LawyerDto> UpdateLawyerInfoAsync(LawyerDto lawyerDto);

        /// <summary>
        /// Изменяет пароль юриста
        /// </summary>
        /// <param name="lawyer">Юрист</param>
        /// <returns>Модель юриста с изменёнными полями</returns>
        public Task<LawyerDto> ChangePasswordAsync(Lawyer lawyer);
    }
}
