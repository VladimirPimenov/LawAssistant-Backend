using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using LawAssistant.Api.Extensions;
using LawAssistant.Application.Contracts;
using LawAssistant.Application.Models;

namespace LawAssistant.Api.Controllers
{
    /// <summary>
    /// Контроллер для общих операций с аккаунтами
    /// </summary>
    [ApiController, Route("accounts")]
    public class AccountController(
        IAccountService accountService,
        INotificationService notificationService) 
        : ControllerBase
    {
        /// <summary>
        /// Изменяет данные аккаунта
        /// </summary>
        /// <param name="account">Обновлённые данные аккаунта</param>
        /// <returns>Изменённая модель аккаунта</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateAccountAsync(AccountDto account)
        {
            var userId = User.GetUserId();
            if(userId == null || userId != account.AccountId)
                return Forbid();
        
            var updatedLawyer = await accountService.UpdateAccountInfoAsync(account);

            return updatedLawyer == null ? BadRequest() : Ok(updatedLawyer);
        }
    
        /// <summary>
        /// Возвращает уведомления аккаунта
        /// </summary>
        /// <param name="accountId">Идентификатор аккаунта</param>
        /// <returns>Список уведомлений</returns>
        [Authorize]
        [HttpGet("{accountId}/notifications")]
        public async Task<IActionResult> GetLawyerNotificationsAsync([FromRoute] int accountId)
        {
            var userId = User.GetUserId();
            if(userId == null || userId != accountId)
                return Forbid();
        
            var notifications = await notificationService.GetAccountNotificationsAsync(accountId);

            return notifications == null ? BadRequest() : Ok(notifications);
        }
    }
}