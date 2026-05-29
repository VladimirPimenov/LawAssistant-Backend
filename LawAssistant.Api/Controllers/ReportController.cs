using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using LawAssistant.Application.Contracts;
using LawAssistant.Domain.Entities;

namespace LawAssistant.Api.Controllers
{
	/// <summary>
	/// Контроллер для формирования отчётов о сопоставлении договора со статьями законодательных актов.
	/// </summary>
	[ApiController, Route("reports")]
	public class ReportController(
		IReportService reportService)
		: ControllerBase
	{
		/// <summary>
		/// Получает отчёт по идентификатору
		/// </summary>
		/// <param name="reportId">Идентификатор отчёта</param>
		/// <returns>
		/// 200 (Ok) с найденным отчётом.
		/// 404 (NotFound) если отчёт не найден.
		/// </returns>
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
		/// Создаёт отчёт о сопоставлении существующего договора
		/// </summary>
		/// <param name="contractId">Идентификатор договора</param>
		/// <returns>
		/// 200 (Ok) с созданным отчётом.
		/// 400 (BadRequest) при ошибке.
		/// </returns>
		//[Authorize]
		[HttpPost]
		[ProducesResponseType<ComparisonReport>(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> CreateReportForContractAsync([FromQuery] int contractId)
		{
			var createdReport = await reportService.CreateReportAsync(contractId);

			return createdReport == null ? BadRequest() : Ok(createdReport);
		}

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
