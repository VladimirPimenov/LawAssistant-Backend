using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;
using Microsoft.AspNetCore.Http;

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

        public static ReportWithResults CreateReportForView(
            this ComparisonReport report,
            List<ReportParagraph> results)
        {
            var reportWithResults = new ReportWithResults
            {
                ReportId = report.ReportId,
                ReportedDate = report.ReportedDate,
                ContractId = report.ContractId,
                Results = results
                    .OrderBy(rr => rr.Paragraph.ParagraphId)
                    .ToList()
            };

            return reportWithResults;
        }
    }
}
