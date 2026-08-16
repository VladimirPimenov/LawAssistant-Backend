using LawAssistant.Application.Contracts;
using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;

namespace LawAssistant.Application.Services
{
	internal class SiteNotificationService(
        INotificationRepository notificationRepository,
        IAccountService accountService)
        : INotificationService
    {
        public async Task<List<Notification>> GetAccountNotificationsAsync(int accountId)
        {
            var notifications = await notificationRepository.GetAccountNotificationsAsync(accountId);
            return notifications
                .OrderByDescending(n => n.Date)
                .ToList();
		}
            

        public async Task<Notification> CreateNotificationAsync(string notificationText, int accountId)
        {
            var lawyer = await accountService.GetAccountAsync(accountId);
            if (lawyer == null)
                return null;

            var notification = new Notification
            {
                AccountId = accountId,
                Text = notificationText,
                Date = DateTime.Now.ToUniversalTime(),
                IsReaded = false
            };

            var createdNotification = await notificationRepository.CreateNotificationAsync(notification);
            return createdNotification;
        }

        public async Task<Notification> UpdateNotificationAsync(Notification notification)
        {
            notification.Date = notification.Date.ToUniversalTime();

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
