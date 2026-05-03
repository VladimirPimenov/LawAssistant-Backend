using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Contracts
{
    public interface INotificationService
    {
        public Task<List<Notification>> GetLawyerNotificationsAsync(int lawyerId);

        public Task<Notification> CreateNotificationAsync(string notificationText, int lawyerId);

        public Task<Notification> UpdateNotificationAsync(Notification notification);

        public Task<int?> RemoveNotificationAsync(int notificationId);
    }
}
