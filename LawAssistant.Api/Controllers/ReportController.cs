using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using LawAssistant.Application.Contracts;
using LawAssistant.Domain.Entities;

namespace LawAssistant.Api.Controllers
{
	/// <summary>
	/// Контроллер для работы с отчётами
	/// </summary>
	[ApiController, Route("reports")]
	public class ReportController(
		IReportService reportService)
		: ControllerBase
	{
		/// <summary>
		/// Возвращает отчёт
		/// </summary>
		/// <param name="reportId">Идентификатор отчёта</param>
		/// <returns>Найденный отчёт</returns>
		//[Authorize]
		[HttpGet]
		[ProducesResponseType<ComparisonReport>(200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> GetReportAsync([FromQuery] int reportId)
		{
			var report = await reportService.GetReportAsync(reportId);

			return report == null ? NotFound() : Ok(report);
		}

		/// <summary>
		/// Создаёт отчёт о сопоставлении существующего договора со статьями законодательства
		/// </summary>
		/// <param name="contractId">Идентификатор договора</param>
		/// <returns>Созданный отчёт</returns>
		//[Authorize]
		[HttpPost]
		[ProducesResponseType<ComparisonReport>(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> CreateReportForContractAsync([FromQuery] int contractId)
		{
			var createdReport = await reportService.CreateReportAsync(contractId);

			return createdReport == null ? BadRequest() : Ok(createdReport);
		}

		/// <summary>
		/// Удаляет отчёт
		/// </summary>
		/// <param name="reportId">Идентификатор отчёта</param>
		/// <returns>Идентификатор удалённого отчёта</returns>
		//[Authorize]
		[HttpDelete]
		[ProducesResponseType<ComparisonReport>(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> RemoveReportAsync([FromQuery] int reportId)
		{
			int? removedReportId = await reportService.RemoveReportAsync(reportId);

			return removedReportId == null ? BadRequest() : Ok(removedReportId);
		}
	}
}
