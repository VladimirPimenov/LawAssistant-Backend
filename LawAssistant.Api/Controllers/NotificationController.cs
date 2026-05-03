using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using LawAssistant.Application.Contracts;
using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;

namespace LawAssistant.Api.Controllers
{
    [ApiController, Route("notification")]
    public class NotificationController(
        INotificationService notificationService)
        : ControllerBase
    {
        //[Authorize]
        [HttpGet("get-lawyer-notifications")]
        public async Task<IActionResult> GetLawyerNotificationsAsync(int lawyerId)
        {
            var notifications = await notificationService.GetLawyerNotificationsAsync(lawyerId);

            return notifications == null ? BadRequest() : Ok(notifications);
        }

        //[Authorize]
        [HttpPut("update-notification")]
        public async Task<IActionResult> UpdateNotificationAsync(Notification notification)
        {
            var updatedNotification = await notificationService.UpdateNotificationAsync(notification);

            return updatedNotification == null ? BadRequest() : Ok(updatedNotification);
        }

        //[Authorize]
        [HttpDelete("remove-notification")]
        public async Task<IActionResult> RemoveNotificationAsync(int notificationId)
        {
            var removedNotificationId = await notificationService.RemoveNotificationAsync(notificationId);

            return removedNotificationId == null ? BadRequest() : Ok(removedNotificationId);
        }
    }
}
