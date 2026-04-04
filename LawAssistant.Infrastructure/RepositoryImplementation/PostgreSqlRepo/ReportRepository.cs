using Microsoft.EntityFrameworkCore;

using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;
using LawAssistant.Infrastructure.RepositoryImplementation.Models;

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
                .ToListAsync();
        }
    }
}
