using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using LawAssistant.Application.Contracts;
using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;
using LawAssistant.Api.Extensions;

namespace LawAssistant.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы c юристами
    /// </summary>
    [ApiController, Route("lawyers")]
    public class LawyerController(
        ILawyerService lawyerService,
        IContractService contractService,
        INotificationService notificationService,
        IReportService reportService)
        : ControllerBase
    {
        /// <summary>
        /// Возращает список всех юристов
        /// </summary>
        /// <returns>Список юристов</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetLawyersList()
        {
            var lawyers = await lawyerService.GetLawyersListAsync();

            return lawyers == null ? NotFound() : Ok(lawyers);
        }
        /// <summary>
        /// Изменяет данные учётной записи юриста
        /// </summary>
        /// <param name="lawyer">Обновлённые данные юриста</param>
        /// <returns>Изменённая модель юриста</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateLawyerInfoAsync(LawyerDto lawyer)
        {
            var userId = User.GetUserId();
            if(userId == null || userId != lawyer.LawyerId)
                return Forbid();
        
            var updatedLawyer = await lawyerService.UpdateLawyerInfoAsync(lawyer);

            return updatedLawyer == null ? BadRequest() : Ok(updatedLawyer);
        }
        
        /// <summary>
        /// Возвращает договоры юриста
        /// </summary>
        /// <param name="lawyerId">Идентификатор юриста</param>
        /// <returns>Список договоров</returns>
        [Authorize]
        [HttpGet("{lawyerId}/contracts")]
        [ProducesResponseType<List<ContractDto>>(200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> GetLawyerContracts([FromRoute] int lawyerId)
        {
            var userId = User.GetUserId();
            if(userId == null || userId != lawyerId)
                return Forbid();
            
            var lawyerContracts = await contractService.GetLawyerContractsInfoAsync(lawyerId);

            return lawyerContracts == null ? NotFound() : Ok(lawyerContracts);
        }
        
        /// <summary>
        /// Возвращает отчёты юриста
        /// </summary>
        /// <param name="lawyerId">Идентификатор юриста</param>
        /// <returns>Список отчётов</returns>
        [Authorize]
        [HttpGet("{lawyerId}/reports")]
		[ProducesResponseType<ComparisonReport>(200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> GetLawyerReportsAsync([FromRoute] int lawyerId)
		{
            var userId = User.GetUserId();
            if(userId == null || userId != lawyerId)
                return Forbid();
		
			var reports = await reportService.GetLawyerReportsAsync(lawyerId);

			return reports == null ? NotFound() : Ok(reports);
		}
        
        /// <summary>
        /// Возвращает уведомления юриста
        /// </summary>
        /// <param name="lawyerId">Идентификатор юриста</param>
        /// <returns>Список уведомлений</returns>
        [Authorize]
        [HttpGet("{lawyerId}/notifications")]
        public async Task<IActionResult> GetLawyerNotificationsAsync([FromRoute] int lawyerId)
        {
            var userId = User.GetUserId();
            if(userId == null || userId != lawyerId)
                return Forbid();
        
            var notifications = await notificationService.GetLawyerNotificationsAsync(lawyerId);

            return notifications == null ? BadRequest() : Ok(notifications);
        }
    }
}
