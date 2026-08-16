using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using LawAssistant.Application.Contracts;
using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;
using LawAssistant.Api.Extensions;

namespace LawAssistant.Api.Controllers
{
    /// <summary>
    /// Контроллер для операций с аккаунтами юристов
    /// </summary>
    [ApiController, Route("lawyers")]
    public class LawyerController(ILawyerService lawyerService) : ControllerBase
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
            
            var lawyerContracts = await lawyerService.GetLawyerContractsAsync(lawyerId);

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
		
			var reports = await lawyerService.GetLawyerReportsAsync(lawyerId);

			return reports == null ? NotFound() : Ok(reports);
		}
    }
}
