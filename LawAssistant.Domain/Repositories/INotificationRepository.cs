using LawAssistant.Domain.Entities;

namespace LawAssistant.Domain.Repositories
{
    /// <summary>
    /// Интерфейс для работы с уведомлениями
    /// </summary>
    public interface INotificationRepository
    {
        public Task<Notification> GetNotificationAsync(int notificationId);

        public Task<List<Notification>> GetAccountNotificationsAsync(int accountId);

        public Task<Notification> CreateNotificationAsync(Notification notification);

        public Task<Notification> UpdateNotificationAsync(Notification notification);

        public Task<int?> RemoveNotificationAsync(Notification notification);
    }
}
