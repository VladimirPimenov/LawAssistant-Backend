using Microsoft.AspNetCore.Mvc;

using LawAssistant.Application.Contracts;
using LawAssistant.Domain.Entities;

namespace LawAssistant.Api.Controllers
{
    [ApiController, Route("notifications")]
    public class NotificationController(
        INotificationService notificationService)
        : ControllerBase
    {
        //[Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateNotificationAsync(Notification notification)
        {
            var updatedNotification = await notificationService.UpdateNotificationAsync(notification);

            return updatedNotification == null ? BadRequest() : Ok(updatedNotification);
        }

        //[Authorize]
        [HttpDelete]
        public async Task<IActionResult> RemoveNotificationAsync(int notificationId)
        {
            var removedNotificationId = await notificationService.RemoveNotificationAsync(notificationId);

            return removedNotificationId == null ? BadRequest() : Ok(removedNotificationId);
        }
    }
}
