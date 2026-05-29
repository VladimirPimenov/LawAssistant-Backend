namespace LawAssistant.Infrastructure.RepositoryImplementation.Models
{
	internal record LawyerReport
    {
        public int LawyerReportId { get; init; }

        public int LawyerId { get; init; }

        public int ReportId { get; init; }
    }
}
