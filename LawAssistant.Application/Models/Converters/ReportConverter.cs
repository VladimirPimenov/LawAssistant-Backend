using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Models
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
