namespace LawAssistant.Infrastructure.RepositoryImplementation.Models
{
	internal record ReportResult
	{
		public int ReportResultId { get; init; }

		public int ReportId { get; init; }

		public int ResultId { get; init; }
	}
}
