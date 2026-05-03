using LawAssistant.Application.Contracts;
using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;

namespace LawAssistant.Application.Services
{
    public class SiteNotificationService(
        INotificationRepository notificationRepository,
        ILawyerService lawyerService)
        : INotificationService
    {
        public async Task<List<Notification>> GetLawyerNotificationsAsync(int lawyerId) =>
            await notificationRepository.GetLawyerNotificationsAsync(lawyerId);

        public async Task<Notification> CreateNotificationAsync(string notificationText, int lawyerId)
        {
            var lawyer = await lawyerService.GetLawyerAsync(lawyerId);
            if (lawyer == null)
                return null;

            var notification = new Notification
            {
                LawyerId = lawyerId,
                Text = notificationText,
                Date = DateTime.Now.ToUniversalTime(),
                IsReaded = false
            };

            var createdNotification = await notificationRepository.CreateNotificationAsync(notification);
            return createdNotification;
        }

        public async Task<Notification> UpdateNotificationAsync(Notification notification)
        {
            var dbNotification = await notificationRepository.GetNotificationAsync(notification.NotificationId);
            
            if (dbNotification.LawyerId != notification.LawyerId)
                return null;

            var updatedNotification = await notificationRepository.UpdateNotificationAsync(notification);
            return updatedNotification;
        }

        public async Task<int?> RemoveNotificationAsync(int notificationId)
        {
            var notification = await notificationRepository.GetNotificationAsync(notificationId);
            if(notification == null) 
                return null;

            var removedNoticeId = await notificationRepository.RemoveNotificationAsync(notification);
            return removedNoticeId;
        }
    }
}
