using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LawAssistant.Infrastructure.RepositoryImplementation.PostgreSqlRepo
{
	internal class NotificationRepository(
        PostgreSqlDbContext dbContext)
        : INotificationRepository
    {
        public async Task<Notification> GetNotificationAsync(int notificationId)
        {
            return await dbContext.Notification.FindAsync(notificationId);
        }

        public async Task<List<Notification>> GetAccountNotificationsAsync(int accountId)
        {
            var notifications = await dbContext.Notification
                .Where(n => n.AccountId == accountId)
                .ToListAsync();

            return notifications;
        }

        public async Task<Notification> CreateNotificationAsync(Notification notification)
        {
            dbContext.Notification.Add(notification);
            await dbContext.SaveChangesAsync();

            return notification;
        }

        public async Task<Notification> UpdateNotificationAsync(Notification updatedNotification)
        {
            dbContext.Notification.Update(updatedNotification);
            await dbContext.SaveChangesAsync();

            return updatedNotification;
        }

        public async Task<int?> RemoveNotificationAsync(Notification notification)
        {
            dbContext.Notification.Remove(notification);
            await dbContext.SaveChangesAsync();

            return notification.NotificationId;
        }
    }
}
