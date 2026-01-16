namespace LawAssistant.Domain.Entities
{
	public class ComparisonReport
	{
		public int ReportId { get; set; }

		public int ResultId { get; set; }

		public int LawyerId { get; set; }

		public DateTime ReportedDate { get; set; }
	}
}
