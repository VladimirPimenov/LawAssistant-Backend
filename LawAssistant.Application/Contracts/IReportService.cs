using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Contracts
{
	public interface IReportService
	{
		public Task<ComparisonReport> GetReportAsync(int reportId);

		public Task<List<ReportDto>> GetContractReportsAsync(int contractId);

		public Task<List<ReportDto>> GetLawyerReportsAsync(int lawyerId);

		public Task<ComparisonReport> CreateReportAsync(int contractId);

		public Task<int?> RemoveReportAsync(int reportId);
	}
}
