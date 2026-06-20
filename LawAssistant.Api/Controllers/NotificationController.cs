using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using LawAssistant.Application.Contracts;
using LawAssistant.Domain.Entities;

namespace LawAssistant.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с уведомлениями
    /// </summary>
    /// <param name="notificationService"></param>
    [ApiController, Route("notifications")]
    public class NotificationController(
        INotificationService notificationService)
        : ControllerBase
    {
        /// <summary>
        /// Изменяет данные уведомления
        /// </summary>
        /// <param name="notification">Уведомление с обновлёнными полями</param>
        /// <returns>Изменённое уведомление</returns>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateNotificationAsync(Notification notification)
        {
            var updatedNotification = await notificationService.UpdateNotificationAsync(notification);

            return updatedNotification == null ? BadRequest() : Ok(updatedNotification);
        }

        /// <summary>
        /// Удаляет уведомление
        /// </summary>
        /// <param name="notificationId">Идентификатор уведомления</param>
        /// <returns>Идентификатор удалённого уведомления</returns>
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> RemoveNotificationAsync(int notificationId)
        {
            var removedNotificationId = await notificationService.RemoveNotificationAsync(notificationId);

            return removedNotificationId == null ? BadRequest() : Ok(removedNotificationId);
        }
    }
}
