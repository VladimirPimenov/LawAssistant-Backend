using LawAssistant.Domain.Entities;

namespace LawAssistant.Infrastructure.RepositoryImplementation.Models
{
	public record ReportResult
	{
		public int ReportResultId { get; init; }

		public int ReportId { get; init; }

		public int ResultId { get; init; }
	}
}
