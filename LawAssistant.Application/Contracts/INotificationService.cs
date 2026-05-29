using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Contracts
{
    /// <summary>
    /// Сервис для работы с уведомлениями
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Возвращает уведомления юриста
        /// </summary>
        /// <param name="lawyerId">Идентификатор юриста</param>
        /// <returns>Список уведомлений</returns>
        public Task<List<Notification>> GetLawyerNotificationsAsync(int lawyerId);

        /// <summary>
        /// Создает уведомление для юриста
        /// </summary>
        /// <param name="notificationText">Текст уведомления</param>
        /// <param name="lawyerId">Юрист, которому предназначено уведомление</param>
        /// <returns>Уведомление</returns>
        public Task<Notification> CreateNotificationAsync(string notificationText, int lawyerId);

        /// <summary>
        /// Изменяет уведомление
        /// </summary>
        /// <param name="notification">Уведомление с обновлёнными полями</param>
        /// <returns>Изменённое уведомление</returns>
        public Task<Notification> UpdateNotificationAsync(Notification notification);

        /// <summary>
        /// Удаляет уведомление
        /// </summary>
        /// <param name="notificationId">Идентификатор уведомления</param>
        /// <returns>Идентификатор удалённого уведомления</returns>
        public Task<int?> RemoveNotificationAsync(int notificationId);
    }
}
