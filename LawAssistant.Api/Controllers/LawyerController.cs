using Microsoft.AspNetCore.Mvc;

using LawAssistant.Application.Contracts;
using LawAssistant.Application.Models;
using Microsoft.AspNetCore.Authorization;

namespace LawAssistant.Api.Controllers
{
    /// <summary>
    /// Контроллер для изменения данных учётных записей юристов.
    /// </summary>
    [ApiController, Route("lawyer")]
    public class LawyerController(
        ILawyerService lawyerService)
        : ControllerBase
    {
        /// <summary>
        /// Изменяет имя и фамилию юриста.
        /// </summary>
        /// <param name="lawyer">Обновлённые данные юриста</param>
        /// <returns>
        /// 200 (Ок) с обновлённым юристом.
        /// 400 (BadRequest) при ошибке.
        /// </returns>
        [Authorize]
        [HttpPost("update-lawyer")]
        public async Task<IActionResult> UpdateLawyerInfoAsync(LawyerDto lawyer)
        {
            var updatedLawyer = await lawyerService.UpdateLawyerInfoAsync(lawyer);

            return updatedLawyer == null ? BadRequest() : Ok(updatedLawyer);
        }
    }
}
