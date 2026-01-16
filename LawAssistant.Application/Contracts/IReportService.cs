using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Contracts
{
	public interface IReportService
	{
		public Task<ComparisonReport> GetReportForContractAsync(int contractId);

		public Task<ComparisonReport> CreateComparisonReportAsync(int contractId);

		public Task<int> RemoveComparisonReportAsync(int reportId);
	}
}
