using Microsoft.AspNetCore.Mvc;

using LawAssistant.Application.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace LawAssistant.Api.Controllers
{
	[ApiController, Route("report")]
	public class ReportController(
		IReportService reportService)
		: ControllerBase
	{
		[Authorize]
		[HttpGet("get-report")] 
		public async Task<IActionResult> GetReportAsync(int reportId)
		{
			var report = await reportService.GetReportAsync(reportId);

			return report == null ? NotFound() : Ok(report);
		}

		[Authorize]
		[HttpPost("create-report")]
		public async Task<IActionResult> CreateReportForContractAsync(int contractId)
		{
			var createdReport = await reportService.CreateComparisonReportAsync(contractId);

			return createdReport == null ? BadRequest() : Ok(createdReport);
		}
	}
}
