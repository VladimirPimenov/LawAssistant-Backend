using DocumentFormat.OpenXml.Bibliography;
using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;
using LawAssistant.Infrastructure.RepositoryImplementation.Models;
using Microsoft.EntityFrameworkCore;

namespace LawAssistant.Infrastructure.RepositoryImplementation.PostgreSqlRepo
{
    public class ReportRepository(
        PostgreSqlDbContext dbContext)
        : IReportRepository
    {
        public async Task<ComparisonReport> GetReportAsync(int reportId)
        {
            return await dbContext.ComparisonReport.FindAsync(reportId);
        }

        public async Task<ComparisonReport> CreateReportAsync(ComparisonReport report)
        {
            dbContext.ComparisonReport.Add(report);
            await dbContext.SaveChangesAsync();

            return report;
        }

        public async Task<int> RemoveReportAsync(ComparisonReport report)
        {
            await RemoveReportResultsAsync(report.ReportId);
            await dbContext.SaveChangesAsync();

            dbContext.ComparisonReport.Remove(report);
            await dbContext.SaveChangesAsync();

            return report.ReportId;
        }

        public async Task AddResultToReportAsync(ComparisonResult result, ComparisonReport report)
        {
            var reportResult = new ReportResult
            {
                ReportId = report.ReportId,
                ResultId = result.ResultId
            };

            dbContext.ReportResult.Add(reportResult);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<ComparisonResult>> GetReportResultsAsync(ComparisonReport report)
        {
            var reportResultsId = await dbContext.ReportResult
                .Where(rr => rr.ReportId == report.ReportId)
                .Select(rr => rr.ResultId)
                .ToListAsync();

            return await dbContext.ComparisonResult
                .Where(res => reportResultsId.Contains(res.ResultId))
                .Include(res => res.ContractParagraph)
                .ToListAsync();
        }

        private async Task RemoveReportResultsAsync(int reportId)
        {
			var reportResults = await dbContext.ReportResult
				.Where(r => r.ReportId == reportId)
				.ToListAsync();

			foreach (var result in reportResults)
			{
				dbContext.ReportResult.Remove(result);
			}
		}

		public async Task AddReportToLawyerAsync(ComparisonReport report, Lawyer lawyer)
		{
            var lawyerReport = new LawyerReport
            {
                ReportId = report.ReportId,
                LawyerId = lawyer.LawyerId
            };

            dbContext.LawyerReport.Add(lawyerReport);
            await dbContext.SaveChangesAsync();
		}

		public async Task RemoveReportFromLawyerAsync(ComparisonReport report, Lawyer lawyer)
		{
            var lawyerReport = await dbContext.LawyerReport
                                .FirstOrDefaultAsync(lr =>
                                lr.LawyerId == lawyer.LawyerId
                                && lr.ReportId == report.ReportId);

			dbContext.LawyerReport.Remove(lawyerReport);
			await dbContext.SaveChangesAsync();
		}

		public async Task<List<Lawyer>> GetReportLawyersAsync(ComparisonReport report)
		{
			var lawyersId = await dbContext.LawyerReport
                .Where(lr => lr.ReportId == report.ReportId)
                .Select(lr => lr.LawyerId)
                .ToListAsync();

            return await dbContext.Lawyer
                .Where(l => lawyersId.Contains(l.LawyerId))
                .ToListAsync();
		}

		public async Task<List<ComparisonReport>> GetContractReportsAsync(int contractId)
		{
            return await dbContext.ComparisonReport
                .Where(r => r.ContractId == contractId)
                .ToListAsync();
		}

		public async Task<List<ComparisonReport>> GetLawyerReportsAsync(int lawyerId)
		{
			var reportsId = await dbContext.LawyerReport
				.Where(lr => lr.LawyerId == lawyerId)
                .Select(lr => lr.ReportId)
				.ToListAsync();

            return await dbContext.ComparisonReport
                .Where(r => reportsId.Contains(r.ReportId))
                .ToListAsync();
		}
	}
}
