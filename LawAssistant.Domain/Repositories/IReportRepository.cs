using LawAssistant.Domain.Entities;

namespace LawAssistant.Domain.Repositories
{
	public interface IReportRepository
	{
		public Task<ComparisonReport> GetReportAsync(int reportId);

		public Task<ComparisonReport> CreateReportAsync(ComparisonReport report);

		public Task<int> RemoveReportAsync(ComparisonReport report);

		public Task AddResultToReportAsync(ComparisonResult result, ComparisonReport report);

		public Task<List<ComparisonResult>> GetReportResultsAsync(ComparisonReport report);
	}
}
