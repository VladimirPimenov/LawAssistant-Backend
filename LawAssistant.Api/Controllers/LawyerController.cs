using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using LawAssistant.Application.Contracts;
using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;

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
        /// Получает список всех юристов.
        /// </summary>
        /// <returns>
        /// 200 (Ok) со списком юристов.
        /// 404 (NotFound) если ничего не найдено (проблема с БД).
        /// </returns>
        //[Authorize]
        [HttpGet]
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
        [HttpPost]
        public async Task<IActionResult> UpdateLawyerInfoAsync(LawyerDto lawyer)
        {
            var updatedLawyer = await lawyerService.UpdateLawyerInfoAsync(lawyer);

            return updatedLawyer == null ? BadRequest() : Ok(updatedLawyer);
        }
        
        /// <summary>
        /// Получает договоры юриста по его идентификатору.
        /// </summary>
        /// <param name="lawyerId">Идентификатор юриста</param>
        /// <returns>
        /// 200 (Ok) со списком договоров.
        /// 404 (NotFound) если договоры не найдены.
        /// </returns>
        //[Authorize]
        [HttpGet("{lawyerId}/contracts")]
        [ProducesResponseType<List<ContractDto>>(200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> GetLawyerContracts([FromRoute] int lawyerId)
        {
            var lawyerContracts = await contractService.GetLawyerContractsInfoAsync(lawyerId);

            return lawyerContracts == null ? NotFound() : Ok(lawyerContracts);
        }
        
        //[Authorize]
        [HttpGet("{lawyerId}/reports")]
		[ProducesResponseType<ComparisonReport>(200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> GetLawyerReportsAsync([FromRoute] int lawyerId)
		{
			var reports = await reportService.GetLawyerReportsAsync(lawyerId);

			return reports == null ? NotFound() : Ok(reports);
		}
        
        //[Authorize]
        [HttpGet("{lawyerId}/notifications")]
        public async Task<IActionResult> GetLawyerNotificationsAsync([FromRoute] int lawyerId)
        {
            var notifications = await notificationService.GetLawyerNotificationsAsync(lawyerId);

            return notifications == null ? BadRequest() : Ok(notifications);
        }
    }
}
