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
        /// Возвращает уведомления пользователя
        /// </summary>
        /// <param name="accountId">Идентификатор пользователя</param>
        /// <returns>Список уведомлений</returns>
        public Task<List<Notification>> GetAccountNotificationsAsync(int accountId);

        /// <summary>
        /// Создает уведомление для пользователя
        /// </summary>
        /// <param name="notificationText">Текст уведомления</param>
        /// <param name="accountId">Пользователь, которому предназначено уведомление</param>
        /// <returns>Уведомление</returns>
        public Task<Notification> CreateNotificationAsync(string notificationText, int accountId);

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
