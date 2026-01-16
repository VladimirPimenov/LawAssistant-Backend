using LawAssistant.Domain.Entities;

namespace LawAssistant.Domain.Repositories
{
	public interface IReportRepository
	{
		public Task<ComparisonReport> GetReportAsync(int reportId);

		public Task<ComparisonReport> CreateReportAsync(int contractId);

		public Task<int> RemoveReportAsync(int reportId);
	}
}
