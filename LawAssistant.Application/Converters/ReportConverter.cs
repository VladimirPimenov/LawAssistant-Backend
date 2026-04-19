using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Converters
{
	public static class ReportConverter
	{
		public static ReportDto ConvertToDto(this ComparisonReport report, ContractDto contract)
		{
			return new ReportDto
			{
				ReportId = report.ReportId,
				ReportedDate = report.ReportedDate,
				Contract = contract
			};
		}
	}
}
