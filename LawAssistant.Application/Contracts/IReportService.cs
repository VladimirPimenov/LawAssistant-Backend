using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Contracts
{
	public interface IReportService
	{
		public Task<ComparisonReport> GetReportAsync(int reportId);

		public Task<ComparisonReport> CreateComparisonReportAsync(int contractId);

		public Task<int> RemoveComparisonReportAsync(int reportId);
	}
}
