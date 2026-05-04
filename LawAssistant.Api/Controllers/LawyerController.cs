using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using LawAssistant.Application.Contracts;
using LawAssistant.Application.Models;

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
        /// Получает список всех юристов.
        /// </summary>
        /// <returns>
        /// 200 (Ok) со списком юристов.
        /// 404 (NotFound) если ничего не найдено (проблема с БД).
        /// </returns>
        //[Authorize]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetLawyersList()
        {
            var lawyers = await lawyerService.GetLawyersListAsync();

            return lawyers == null ? NotFound() : Ok(lawyers);
        }
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
