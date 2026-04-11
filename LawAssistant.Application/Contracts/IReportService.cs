using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Contracts
{
	public interface IReportService
	{
		public Task<ComparisonReport> GetReportAsync(int reportId);

		public Task<ComparisonReport> CreateReportAsync(int contractId);

		public Task<int?> RemoveReportAsync(int reportId);
	}
}
